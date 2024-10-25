﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by Reqnroll (https://www.reqnroll.net/).
//      Reqnroll Version:2.0.0.0
//      Reqnroll Generator Version:2.0.0.0
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace Cbr.Specs.Feature
{
    using Reqnroll;
    using System;
    using System.Linq;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Reqnroll", "2.0.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public partial class КурсВалютЦБРФFeature : object, Xunit.IClassFixture<КурсВалютЦБРФFeature.FixtureData>, Xunit.IAsyncLifetime
    {
        
        private static global::Reqnroll.ITestRunner testRunner;
        
        private static string[] featureTags = ((string[])(null));
        
        private Xunit.Abstractions.ITestOutputHelper _testOutputHelper;
        
#line 1 "Cbr.feature"
#line hidden
        
        public КурсВалютЦБРФFeature(КурсВалютЦБРФFeature.FixtureData fixtureData, Xunit.Abstractions.ITestOutputHelper testOutputHelper)
        {
            this._testOutputHelper = testOutputHelper;
        }
        
        public static async System.Threading.Tasks.Task FeatureSetupAsync()
        {
            testRunner = global::Reqnroll.TestRunnerManager.GetTestRunnerForAssembly();
            global::Reqnroll.FeatureInfo featureInfo = new global::Reqnroll.FeatureInfo(new System.Globalization.CultureInfo("ru-RU"), "Feature", "курс валют ЦБ РФ", "\tКак пользователь\r\n\tЯ хочу просматривать валютные пары\r\n\tЧтобы быть в курсе актуа" +
                    "льных и прошедших котировок", global::Reqnroll.ProgrammingLanguage.CSharp, featureTags);
            await testRunner.OnFeatureStartAsync(featureInfo);
        }
        
        public static async System.Threading.Tasks.Task FeatureTearDownAsync()
        {
            await testRunner.OnFeatureEndAsync();
            global::Reqnroll.TestRunnerManager.ReleaseTestRunner(testRunner);
            testRunner = null;
        }
        
        public async System.Threading.Tasks.Task TestInitializeAsync()
        {
        }
        
        public async System.Threading.Tasks.Task TestTearDownAsync()
        {
            await testRunner.OnScenarioEndAsync();
        }
        
        public void ScenarioInitialize(global::Reqnroll.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioInitialize(scenarioInfo);
            testRunner.ScenarioContext.ScenarioContainer.RegisterInstanceAs<Xunit.Abstractions.ITestOutputHelper>(_testOutputHelper);
        }
        
        public async System.Threading.Tasks.Task ScenarioStartAsync()
        {
            await testRunner.OnScenarioStartAsync();
        }
        
        public async System.Threading.Tasks.Task ScenarioCleanupAsync()
        {
            await testRunner.CollectScenarioErrorsAsync();
        }
        
        async System.Threading.Tasks.Task Xunit.IAsyncLifetime.InitializeAsync()
        {
            await this.TestInitializeAsync();
        }
        
        async System.Threading.Tasks.Task Xunit.IAsyncLifetime.DisposeAsync()
        {
            await this.TestTearDownAsync();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="импорт из файла, просмотр всех валютных пар к рублю за указанную дату")]
        [Xunit.TraitAttribute("FeatureTitle", "курс валют ЦБ РФ")]
        [Xunit.TraitAttribute("Description", "импорт из файла, просмотр всех валютных пар к рублю за указанную дату")]
        [Xunit.TraitAttribute("Category", "positive")]
        public async System.Threading.Tasks.Task ИмпортИзФайлаПросмотрВсехВалютныхПарКРублюЗаУказаннуюДату()
        {
            string[] tagsOfScenario = new string[] {
                    "positive"};
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            global::Reqnroll.ScenarioInfo scenarioInfo = new global::Reqnroll.ScenarioInfo("импорт из файла, просмотр всех валютных пар к рублю за указанную дату", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 8
this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((global::Reqnroll.TagHelper.ContainsIgnoreTag(scenarioInfo.CombinedTags) || global::Reqnroll.TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                await this.ScenarioStartAsync();
#line 9
 await testRunner.GivenAsync("я импортировал курсы из файла за дату \"2008-08-26\"", ((string)(null)), ((global::Reqnroll.Table)(null)), "Пусть ");
#line hidden
#line 10
 await testRunner.WhenAsync("я запрашиваю курсы за дату \"2008-08-26\"", ((string)(null)), ((global::Reqnroll.Table)(null)), "Когда ");
#line hidden
#line 11
 await testRunner.ThenAsync("курсы имеют дату \"2008-08-26\"", ((string)(null)), ((global::Reqnroll.Table)(null)), "Тогда ");
#line hidden
#line 12
 await testRunner.AndAsync("получено курсов в количестве 18", ((string)(null)), ((global::Reqnroll.Table)(null)), "И ");
#line hidden
#line 13
 await testRunner.AndAsync("элемент №1 курсов имеет код \"AUD\" с названием \"Австралийский доллар\" и обменом 21" +
                        ",1568", ((string)(null)), ((global::Reqnroll.Table)(null)), "И ");
#line hidden
#line 14
 await testRunner.AndAsync("элемент №2 курсов имеет код \"BYR\" с названием \"Белорусских рублей\" и обменом 0,01" +
                        "15714", ((string)(null)), ((global::Reqnroll.Table)(null)), "И ");
#line hidden
            }
            await this.ScenarioCleanupAsync();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="импорт из памяти, просмотр всех валютных пар к рублю за указанную дату")]
        [Xunit.TraitAttribute("FeatureTitle", "курс валют ЦБ РФ")]
        [Xunit.TraitAttribute("Description", "импорт из памяти, просмотр всех валютных пар к рублю за указанную дату")]
        [Xunit.TraitAttribute("Category", "positive")]
        public async System.Threading.Tasks.Task ИмпортИзПамятиПросмотрВсехВалютныхПарКРублюЗаУказаннуюДату()
        {
            string[] tagsOfScenario = new string[] {
                    "positive"};
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            global::Reqnroll.ScenarioInfo scenarioInfo = new global::Reqnroll.ScenarioInfo("импорт из памяти, просмотр всех валютных пар к рублю за указанную дату", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 17
this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((global::Reqnroll.TagHelper.ContainsIgnoreTag(scenarioInfo.CombinedTags) || global::Reqnroll.TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                await this.ScenarioStartAsync();
#line 18
 await testRunner.GivenAsync("я импортировал курсы из памяти за дату \"2024-10-11\"", ((string)(null)), ((global::Reqnroll.Table)(null)), "Пусть ");
#line hidden
#line 19
 await testRunner.WhenAsync("я запрашиваю курсы за дату \"2024-10-11\"", ((string)(null)), ((global::Reqnroll.Table)(null)), "Когда ");
#line hidden
#line 20
 await testRunner.ThenAsync("курсы имеют дату \"2024-10-11\"", ((string)(null)), ((global::Reqnroll.Table)(null)), "Тогда ");
#line hidden
#line 21
 await testRunner.AndAsync("получено курсов в количестве 2", ((string)(null)), ((global::Reqnroll.Table)(null)), "И ");
#line hidden
#line 22
 await testRunner.AndAsync("элемент №1 курсов имеет код \"HKD\" с названием \"Гонконгский доллар\" и обменом 12,3" +
                        "907", ((string)(null)), ((global::Reqnroll.Table)(null)), "И ");
#line hidden
#line 23
 await testRunner.AndAsync("элемент №2 курсов имеет код \"JPY\" с названием \"Японских иен\" и обменом 0,647076", ((string)(null)), ((global::Reqnroll.Table)(null)), "И ");
#line hidden
            }
            await this.ScenarioCleanupAsync();
        }
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("Reqnroll", "2.0.0.0")]
        [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
        public class FixtureData : object, Xunit.IAsyncLifetime
        {
            
            async System.Threading.Tasks.Task Xunit.IAsyncLifetime.InitializeAsync()
            {
                await КурсВалютЦБРФFeature.FeatureSetupAsync();
            }
            
            async System.Threading.Tasks.Task Xunit.IAsyncLifetime.DisposeAsync()
            {
                await КурсВалютЦБРФFeature.FeatureTearDownAsync();
            }
        }
    }
}
#pragma warning restore
#endregion
