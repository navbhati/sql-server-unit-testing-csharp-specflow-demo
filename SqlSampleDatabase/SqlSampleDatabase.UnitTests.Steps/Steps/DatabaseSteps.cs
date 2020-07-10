using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using SqlSampleDatabase.UnitTests.Framework.Helpers;
using SqlSampleDatabase.UnitTests.Steps.Helpers;
using SqlSampleDatabase.UnitTests.Steps.Steps;

namespace SqlSampleDatabase.UnitTests.Steps
{
    public sealed class DatabaseSteps : StepsBase
    {
        public DatabaseSteps(Context context) : base(context)
        {
        }

        [Given(@"the following tables on '(.*)' are empty:")]
        public async Task GivenTheFollowingTablesOnTheDatabaseAreEmpty(string databaseName, Table tableNames)
        {
            var tableNamesAsStrings = tableNames.Rows.Select(r => r["table name"]);
            foreach (var tableName in tableNamesAsStrings)
            {
                await Context.Database.TruncateAsync(databaseName, tableName);
            }
        }

        [Given(@"the table '(.*)' on '(.*)' contains the data:")]
        public async Task GivenTheTableOnTheDatabaseOnlyContainsTheData(string tableName, string databaseName, TypedTable contents)
        {
            await Context.Database.InsertAsync(databaseName, tableName, contents);
        }

        [When(@"the '(.*)' stored procedure on '(.*)' with params is executed:")]
        public async Task WhenTheStoredProcedureOnTheDatabaseWithParamsIsExecuted(string procName, string databaseName, Table parametersTable)
        {
            Context.ResultSet = await Context.Database.ExecProcAsync(databaseName, procName, parametersTable);
        }

        [Then(@"the view '(.*)' on '(.*)' should only contain the data without strict ordering:")]
        [Then(@"the table '(.*)' on '(.*)' should only contain the data without strict ordering:")]
        public async Task ThenTheTableOnTheDatabaseShouldOnlyContainTheDataNoOrdering(string tableName, string databaseName, TypedTable expectedRows)
        {
            var actualRows = await Context.Database.ReadAllAsync(databaseName, tableName);
            CustomAssertions.ResultsAreEqual(expectedRows, actualRows, false);
        }
    }
}
