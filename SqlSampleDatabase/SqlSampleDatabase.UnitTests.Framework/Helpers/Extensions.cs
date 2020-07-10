using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TechTalk.SpecFlow;

namespace SqlSampleDatabase.UnitTests.Framework.Helpers
{
    public static class Extensions
    {
        public static string ParsePlaceholderExpressions(this string expr)
        {
            if (expr == "${null}")
                return null;
            if (expr != null && !expr.Contains("UtcNowDateKey") && expr.Contains("UtcNowDate"))
                return ParseUtcNowDateExpression(expr, "yyyy-MM-dd");
            if (expr != null && expr.Contains("UtcNowDateKey"))
                return ParseUtcNowDateExpression(expr, "yyyyMMdd");
            return expr;
        }

        public static IDictionary<string, object> ToDictionary(this Table table)
        {
            return table.Rows.ToDictionary(
                r => r["ParameterName"],
                r => Convert.ChangeType(r["Value"], GetTypeFromSqlType(r["Type"]))
            );
        }

        private static string ParseUtcNowDateExpression(string expr, string format)
        {
            var regex = new Regex(@".*([-+]\d+)");
            var matches = regex.Matches(expr);
            if (matches.Count == 0) return DateTime.UtcNow.ToString(format);
            var numberOfDays = int.Parse(matches[0].Groups[1].Value);
            return DateTime.UtcNow.AddDays(numberOfDays).ToString(format);
        }

        private static Type GetTypeFromSqlType(string typeName) => IsClrType(typeName) ? Type.GetType(typeName, true) : Converter.TranslateType(typeName);

        private static bool IsClrType(string typeName) => typeName.StartsWith("System", StringComparison.OrdinalIgnoreCase);
    }
}