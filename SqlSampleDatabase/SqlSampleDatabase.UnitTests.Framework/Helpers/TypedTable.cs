using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TechTalk.SpecFlow;

namespace SqlSampleDatabase.UnitTests.Framework.Helpers
{
    public class TypedTable : Table
    {
        private static readonly Regex ColumnHeaderRegex = new Regex(@"^(.+) \[(.+)\]$");

        public TypedTable(Table table) : base(StripTypesFromHeader(RemoveCommentColumns(table.Header)))
        {
            SetupRows(table);
            ReplacePlaceholders();
            SetupColumnTypes(table.Header);
        }

        public Dictionary<string, Type> ColumnTypes { get; private set; }
        public Dictionary<string, string> ColumnSqlTypes { get; private set; }

        private static IEnumerable<string> RemoveCommentColumns(IEnumerable<string> header)
        {
            return header.Where(h => !h.ToLower().StartsWith("comment"));
        }

        private static string[] StripTypesFromHeader(IEnumerable<string> header)
        {
            return header.Select(h => ColumnHeaderRegex.Match(h).Groups[1].Value).ToArray();
        }

        private void SetupRows(Table table)
        {
            foreach (var row in table.Rows)
            {
                var rowWithoutComments = Enumerable.Where<KeyValuePair<string, string>>(row, kvp => !kvp.Key.ToLower().StartsWith("comment"))
                    .Select(kvp => kvp.Value).ToArray();
                AddRow(rowWithoutComments);
            }
        }

        private void ReplacePlaceholders()
        {
            foreach (var row in Rows)
                foreach (var header in row.Keys)
                    row[header] = row[header].ParsePlaceholderExpressions();
        }

        public static DateTime ParseStartOfWeek(DateTime dt, DayOfWeek startOfWeek)
        {
            int diff = (7 + (dt.DayOfWeek - startOfWeek)) % 7;
            return dt.AddDays(-1 * diff).Date;
        }

        private void SetupColumnTypes(IEnumerable<string> tableHeader)
        {
            ColumnTypes = new Dictionary<string, Type>();
            ColumnSqlTypes = new Dictionary<string, string>();
            foreach (var columnHeader in RemoveCommentColumns(tableHeader))
            {
                var regexMatchGroups = ColumnHeaderRegex.Match(columnHeader).Groups;
                if (regexMatchGroups.Count != 3)
                    throw new ArgumentException(
                        $"Unable to extract column name and type from {columnHeader}. Ensure there is a space between the column and the type");

                var columnName = regexMatchGroups[1].Value;
                var columnType = regexMatchGroups[2].Value;

                try
                {
                    ColumnTypes[columnName] = Converter.TranslateType(columnType);
                    ColumnSqlTypes[columnName] = columnType;
                }
                catch (ArgumentException e)
                {
                    throw new ArgumentException($"No translation for column '{columnName}' with '{columnType}' to an actual type has been setup for the TypedTable", e);
                }
            }
        }

        public List<Dictionary<string, object>> ToDictionaries()
        {
            return Enumerable.ToList<Dictionary<string, object>>(Rows.Select(row => Enumerable.ToDictionary<string, string, object>(Header, header => header, header => ConvertToType(row, header))));
        }

        private object ConvertToType(TableRow row, string header)
        {
            var value = row[header];
            if (string.IsNullOrEmpty(value)) return null;

            try
            {
                value = value.Trim();
                var type = ColumnTypes[header];

                if (type == typeof(bool))
                    switch (value)
                    {
                        case "1":
                            return true;
                        case "0":
                            return false;
                        default:
                            return bool.Parse(value);
                    }

                if (type == typeof(TimeSpan))
                    return TimeSpan.Parse(value);
                if (type == typeof(DateTimeOffset))
                    return DateTimeOffset.Parse(value);
                if (type == typeof(CustomDateComparison))
                    return value;
                return Convert.ChangeType(value, type);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error for column '{header}' datatype and value {value}", ex);
            }
        }
    }

    public class CustomDateComparison
    {
    }
}