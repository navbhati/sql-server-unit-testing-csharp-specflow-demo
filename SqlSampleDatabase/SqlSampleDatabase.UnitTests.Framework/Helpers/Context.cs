using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Transactions;
using SqlSampleDatabase.UnitTests.Framework.Database;

namespace SqlSampleDatabase.UnitTests.Framework.Helpers
{
    public class Context
    {
        public SqlDatabase Database;
        public readonly IList<string> Databases = new List<string>();
        public Type Exception { get; set; }
        public TransactionScope TransactionScope { get; set; }
        public IEnumerable<IDictionary<string, object>> ResultSet { get; set; }

        public void Reset()
        {
            Exception = null;
            TransactionScope = null;
        }
    }
}