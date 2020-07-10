using NUnit.Framework;
using System;

namespace SqlSampleDatabase.UnitTests.Configuration
{
    public static class Settings
    {
        public static readonly string ConnectionString = TestContext.Parameters["ConnectionString"] ?? throw new Exception("ConnectionString not set. Please check your .runsettings file");

        public static int SqlCommandTimeoutSeconds = Convert.ToInt32(TestContext.Parameters["SqlCommandTimeoutSeconds"] ?? "60");

        public static bool TransactionScopeEnabled = Convert.ToBoolean(TestContext.Parameters["TransactionScopeEnabled"] ?? "true");
    }
}
