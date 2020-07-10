using System;
using System.Linq;
using System.Transactions;
using TechTalk.SpecFlow;
using SqlSampleDatabase.UnitTests.Configuration;
using SqlSampleDatabase.UnitTests.Framework.Database;
using SqlSampleDatabase.UnitTests.Framework.Helpers;

namespace SqlSampleDatabase.UnitTests.Steps.Hooks
{
    [Binding]
    public sealed class SetUpTearDown
    {
        private static bool _testRunTransactionsEnabled;
        private readonly Context _context;

        public SetUpTearDown(Context context)
        {
            _context = context;
        }

        [BeforeScenario]
        public void BeforeScenario(FeatureInfo featureInfo, ScenarioInfo scenarioInfo)
        {
            _context.Reset();
            _context.Database = new SqlDatabase(Settings.ConnectionString, Settings.SqlCommandTimeoutSeconds);

            var debugTag = featureInfo.Tags.Union(scenarioInfo.Tags).Contains("debug", StringComparer.OrdinalIgnoreCase);

            if (_testRunTransactionsEnabled || !debugTag)
                _context.TransactionScope = new TransactionScope();
        }

        [AfterScenario]
        public void AfterScenario()
        {
            _context.TransactionScope?.Dispose();
        }

        private static bool GetTestRunTransactionsStatus()
        {
#if RELEASE
            return true;
#endif
            return Settings.TransactionScopeEnabled;
        }
    }
}