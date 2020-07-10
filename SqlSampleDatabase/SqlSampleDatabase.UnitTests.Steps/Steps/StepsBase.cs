using TechTalk.SpecFlow;
using SqlSampleDatabase.UnitTests.Framework.Helpers;

namespace SqlSampleDatabase.UnitTests.Steps.Steps
{
    [Binding]
    public class StepsBase
    {
        protected Context Context { get; }
        public StepsBase(Context context)
        {
            Context = context;
        }
    }
}