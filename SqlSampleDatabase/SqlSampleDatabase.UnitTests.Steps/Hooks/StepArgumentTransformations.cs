using TechTalk.SpecFlow;
using SqlSampleDatabase.UnitTests.Framework.Helpers;
using SqlSampleDatabase.UnitTests.Steps.Steps;

namespace SqlSampleDatabase.UnitTests.Steps.Hooks
{
    public class StepArgumentTransformations : StepsBase
    {
        public StepArgumentTransformations(Context context) : base(context)
        {
        }

        [StepArgumentTransformation]
        public TypedTable TableToTypedTable(Table table)
        {
            return new TypedTable(table);
        }
    }
}