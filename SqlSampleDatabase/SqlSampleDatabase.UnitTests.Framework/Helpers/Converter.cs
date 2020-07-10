using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TechTalk.SpecFlow;

namespace SqlSampleDatabase.UnitTests.Framework.Helpers

{
    public static class Converter
    {
        public static Type TranslateType(string type)
        {
            switch (type.ToLower())
            {
                case "varchar":
                case "nvarchar":
                case "string":
                case "nchar":
                case "char":
                    return typeof(string);
                case "bit":
                    return typeof(bool);
                case "tinyint":
                case "smallint":
                    return typeof(short);
                case "int":
                case "int32":
                    return typeof(int);
                case "bigint":
                case "long":
                case "int64":
                    return typeof(long);
                case "decimal":
                    return typeof(decimal);
                case "float":
                    return typeof(double);
                case "time":
                    return typeof(TimeSpan);
                case "date":
                case "datetime":
                    return typeof(DateTime);
                case "timestamp":
                    return typeof(DateTimeOffset);
                case "customdatecomparison":
                    return typeof(CustomDateComparison);
                default:
                    throw new ArgumentException();
            }
        }

        public static string SqlSyntax(string type, string value)
        {
            switch (type.ToLower())
            {
                case "varchar":
                case "nvarchar":
                case "string":
                case "nchar":
                case "char":
                    return $"'{value}'";
                case "bit":
                    return $"CAST('{value}' AS BIT)";
                case "smallint":
                case "int":
                case "int32":
                case "bigint":
                case "long":
                case "int64":
                case "decimal":
                case "float":
                    return $"{value}";
                case "time":
                    return $"CAST('{value}' as TIME)";
                case "date":
                    return $"CAST('{value}' as DATE)";
                case "datetime":
                    return $"CAST('{value}' as DATETIME)";
                case "timestamp":
                    return $"CAST('{value}' as DATETIMEOFFSET)";
                default:
                    throw new ArgumentException();
            }
        }
    }
}
