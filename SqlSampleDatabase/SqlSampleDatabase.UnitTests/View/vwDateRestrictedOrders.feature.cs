﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (http://www.specflow.org/).
//      SpecFlow Version:3.1.0.0
//      SpecFlow Generator Version:3.1.0.0
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace SqlSampleDatabase.UnitTests.View
{
    using TechTalk.SpecFlow;
    using System;
    using System.Linq;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "3.1.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [NUnit.Framework.TestFixtureAttribute()]
    [NUnit.Framework.DescriptionAttribute("vwDateRestrictedOrders")]
    [NUnit.Framework.CategoryAttribute("debug")]
    public partial class VwDateRestrictedOrdersFeature
    {
        
        private TechTalk.SpecFlow.ITestRunner testRunner;
        
        private string[] _featureTags = new string[] {
                "debug"};
        
#line 1 "vwDateRestrictedOrders.feature"
#line hidden
        
        [NUnit.Framework.OneTimeSetUpAttribute()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "vwDateRestrictedOrders", null, ProgrammingLanguage.CSharp, new string[] {
                        "debug"});
            testRunner.OnFeatureStart(featureInfo);
        }
        
        [NUnit.Framework.OneTimeTearDownAttribute()]
        public virtual void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        [NUnit.Framework.SetUpAttribute()]
        public virtual void TestInitialize()
        {
        }
        
        [NUnit.Framework.TearDownAttribute()]
        public virtual void TestTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public virtual void ScenarioInitialize(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioInitialize(scenarioInfo);
            testRunner.ScenarioContext.ScenarioContainer.RegisterInstanceAs<NUnit.Framework.TestContext>(NUnit.Framework.TestContext.CurrentContext);
        }
        
        public virtual void ScenarioStart()
        {
            testRunner.OnScenarioStart();
        }
        
        public virtual void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Check output of view with reduced dataset based on order date")]
        public virtual void CheckOutputOfViewWithReducedDatasetBasedOnOrderDate()
        {
            string[] tagsOfScenario = ((string[])(null));
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Check output of view with reduced dataset based on order date", null, ((string[])(null)));
#line 4
this.ScenarioInitialize(scenarioInfo);
#line hidden
            bool isScenarioIgnored = default(bool);
            bool isFeatureIgnored = default(bool);
            if ((tagsOfScenario != null))
            {
                isScenarioIgnored = tagsOfScenario.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((this._featureTags != null))
            {
                isFeatureIgnored = this._featureTags.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((isScenarioIgnored || isFeatureIgnored))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
                TechTalk.SpecFlow.Table table10 = new TechTalk.SpecFlow.Table(new string[] {
                            "table name"});
                table10.AddRow(new string[] {
                            "dbo.Orders"});
#line 5
 testRunner.Given("the following tables on \'SqlSampleDatabase\' are empty:", ((string)(null)), table10, "Given ");
#line hidden
                TechTalk.SpecFlow.Table table11 = new TechTalk.SpecFlow.Table(new string[] {
                            "OrderId [int]",
                            "CustomerId [nvarchar]",
                            "OrderDate [datetime]",
                            "OrderQuantity [int]"});
                table11.AddRow(new string[] {
                            "5745",
                            "testuser1",
                            "2020/05/06 17:59:59",
                            "4"});
                table11.AddRow(new string[] {
                            "5747",
                            "testuser2",
                            "2020/06/20 16:59:59",
                            "7"});
                table11.AddRow(new string[] {
                            "5747",
                            "testuser3",
                            "2020/07/09 15:59:59",
                            "1"});
#line 8
  testRunner.And("the table \'dbo.Orders\' on \'SqlSampleDatabase\' contains the data:", ((string)(null)), table11, "And ");
#line hidden
                TechTalk.SpecFlow.Table table12 = new TechTalk.SpecFlow.Table(new string[] {
                            "OrderId [int]",
                            "CustomerId [nvarchar]",
                            "OrderDate [datetime]",
                            "OrderQuantity [int]"});
                table12.AddRow(new string[] {
                            "5747",
                            "testuser2",
                            "2020/06/20 16:59:59",
                            "7"});
                table12.AddRow(new string[] {
                            "5747",
                            "testuser3",
                            "2020/07/09 15:59:59",
                            "1"});
#line 13
 testRunner.Then("the view \'presentation.vwDateRestrictedOrders\' on \'SqlSampleDatabase\' should only" +
                        " contain the data without strict ordering:", ((string)(null)), table12, "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion