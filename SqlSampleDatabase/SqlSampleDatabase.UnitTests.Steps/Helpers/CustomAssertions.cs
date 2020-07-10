using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using FluentAssertions;
using FluentAssertions.Equivalency;
using SqlSampleDatabase.UnitTests.Framework.Helpers;

namespace SqlSampleDatabase.UnitTests.Steps.Helpers
{
    public static class CustomAssertions
    {
        public static void ResultsAreEqual(TypedTable expectedRows, IEnumerable<IDictionary<string, object>> actualRows, bool strictOrdering = true)
        {
            var expectedResults = expectedRows.ToDictionaries().ToList();
            expectedResults.Sort(DictionariesComparator.CompareTo);

            var actualResults = actualRows.Select(dic => dic
                    .Where(d => expectedRows.Header.Contains(d.Key))
                    .ToDictionary(d => d.Key, d => d.Value))
                .ToList();
            actualResults.Sort(DictionariesComparator.CompareTo);

            if (strictOrdering)
                actualResults.Should().BeEquivalentTo(expectedResults, options => options
                    .WithStrictOrdering()
                    .Using<object>(CustomDateComparison)
                    .When(info => CustomDateComparisonRequired(info, expectedRows))
                );
            else
                actualResults.Should().BeEquivalentTo(expectedResults, options => options
                    .Using<object>(CustomDateComparison)
                    .When(info => CustomDateComparisonRequired(info, expectedRows))
                );
        }

        public static void CustomDateComparison(IAssertionContext<object> context)
        {
            var actualValue = (DateTime)context.Subject;
            var expectedValue = (string)context.Expectation;
            var expectedDateTime = DateTime.Parse(expectedValue);
            actualValue.Should().Be(expectedDateTime);
        }

        public static bool CustomDateComparisonRequired(IMemberInfo info, TypedTable expectedRows)
        {
            var selectedMemberPathNameRegex = new Regex(@"\[\d+\]\[(.+)\]");
            if (!selectedMemberPathNameRegex.IsMatch(info.SelectedMemberPath)) return false;
            var columnName = selectedMemberPathNameRegex.Match(info.SelectedMemberPath).Groups[1].Value;
            return expectedRows.ColumnTypes[columnName] == typeof(CustomDateComparison);
        }
    }
}