using System;
using System.Collections.Generic;
using System.Linq;

namespace SqlSampleDatabase.UnitTests.Framework.Helpers
{
    public static class DictionariesComparator
    {
        public static int CompareTo(this Dictionary<string, object> first, Dictionary<string, object> second)
        {
            if (first == null) throw new ArgumentException("The first dictionary cannot be null");
            if (second == null) throw new ArgumentException("The second dictionary cannot be null");

            if (!first.Keys.Any()) throw new ArgumentException("The first dictionary must contain keys");
            if (!second.Keys.Any()) throw new ArgumentException("The second dictionary must contain keys");

            if (!first.Keys.SequenceEqual(second.Keys)) throw new ArgumentException("The dictionaries must have the same keys");

            foreach (var key in first.Keys)
            {
                var firstValue = first[key];
                var secondValue = second[key];

                if (firstValue == null && secondValue != null) return -1;
                if (firstValue != null && secondValue == null) return 1;
                if (secondValue == null && firstValue == null) continue;

                if (firstValue.GetType() != secondValue.GetType()) throw new ArgumentException($"The values with the key '{key}' are of different types");

                var comparisonValue = ComparisonValue(firstValue, secondValue);

                if (comparisonValue != 0) return comparisonValue;
            }

            return 0;
        }

        public static int ComparisonValue(object firstValue, object secondValue)
        {
            int comparisonValue;
            var firstValueType = firstValue.GetType();
            if (firstValueType == typeof(string))
            {
                var firstValueTyped = (string)firstValue;
                var secondValueTyped = (string)secondValue;

                comparisonValue = string.Compare(firstValueTyped, secondValueTyped, StringComparison.Ordinal);
            }
            else if (firstValueType == typeof(short))
            {
                var firstValueTyped = (short)firstValue;
                var secondValueTyped = (short)secondValue;

                comparisonValue = firstValueTyped.CompareTo(secondValueTyped);
            }
            else if (firstValueType == typeof(int))
            {
                var firstValueTyped = (int)firstValue;
                var secondValueTyped = (int)secondValue;

                comparisonValue = firstValueTyped.CompareTo(secondValueTyped);
            }
            else if (firstValueType == typeof(long))
            {
                var firstValueTyped = (long)firstValue;
                var secondValueTyped = (long)secondValue;

                comparisonValue = firstValueTyped.CompareTo(secondValueTyped);
            }
            else if (firstValueType == typeof(double))
            {
                var firstValueTyped = (double)firstValue;
                var secondValueTyped = (double)secondValue;

                comparisonValue = firstValueTyped.CompareTo(secondValueTyped);
            }
            else if (firstValueType == typeof(decimal))
            {
                var firstValueTyped = (decimal)firstValue;
                var secondValueTyped = (decimal)secondValue;

                comparisonValue = firstValueTyped.CompareTo(secondValueTyped);
            }
            else if (firstValueType == typeof(float))
            {
                var firstValueTyped = (float)firstValue;
                var secondValueTyped = (float)secondValue;

                comparisonValue = firstValueTyped.CompareTo(secondValueTyped);
            }
            else if (firstValueType == typeof(bool))
            {
                var firstValueTyped = (bool)firstValue;
                var secondValueTyped = (bool)secondValue;

                comparisonValue = firstValueTyped.CompareTo(secondValueTyped);
            }
            else if (firstValueType == typeof(DateTime))
            {
                var firstValueTyped = (DateTime)firstValue;
                var secondValueTyped = (DateTime)secondValue;

                comparisonValue = firstValueTyped.CompareTo(secondValueTyped);
            }
            else if (firstValueType == typeof(DateTimeOffset))
            {
                var firstValueTyped = (DateTimeOffset)firstValue;
                var secondValueTyped = (DateTimeOffset)secondValue;

                comparisonValue = firstValueTyped.CompareTo(secondValueTyped);
            }
            else if (firstValueType == typeof(TimeSpan))
            {
                var firstValueTyped = (TimeSpan)firstValue;
                var secondValueTyped = (TimeSpan)secondValue;

                comparisonValue = firstValueTyped.CompareTo(secondValueTyped);
            }
            else
            {
                throw new ArgumentException("The type of the object is unknown");
            }

            return comparisonValue;
        }
    }
}