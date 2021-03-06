using System.Collections.Generic;
using FubuMVC.Core.View.Model;
using FubuMVC.Spark.SparkModel;
using FubuTestingSupport;
using NUnit.Framework;
using Rhino.Mocks;

namespace FubuMVC.Spark.Tests.SparkModel
{
    [TestFixture]
    public class FubuPartialProviderTester : InteractionContext<FubuPartialProvider>
    {
        private ITemplateDirectoryProvider<ITemplate> _templateDirectoryProvider;
        private string _viewPath;
        private List<string> _sharedTemplates;

        protected override void beforeEach()
        {
            _viewPath = @"_Package\Handlers\Models\test.spark";
            _templateDirectoryProvider = MockFor<ITemplateDirectoryProvider<ITemplate>>();
            _sharedTemplates = new List<string>();
            _templateDirectoryProvider
                .Expect(x => x.SharedViewPathsForOrigin("Package"))
                .Return(_sharedTemplates);
        }

        [Test]
        public void get_paths_includes_default_partial_paths()
        {
            var paths = ClassUnderTest.GetPaths(_viewPath);
            paths.ShouldHaveTheSameElementsAs(new[]
            {
                @"_Package\Handlers\Models",
                @"_Package\Handlers\Models\Shared",
                @"_Package\Handlers",
                @"_Package\Handlers\Shared",
                @"_Package",
                @"_Package\Shared",
                @"",
                @"Shared",
            });
        }

        [Test]
        public void get_paths_also_includes_any_shared_view_paths_for_origin()
        {
            const string sharedTemplate = @"_Global\Shared";
            _sharedTemplates.Add(sharedTemplate);
            var paths = ClassUnderTest.GetPaths(_viewPath);
            paths.ShouldContain(sharedTemplate);
        }

        [Test]
        public void can_parse_origin_from_view_path()
        {
            var inputs = new[]
            {
                @"_Package",
                @"_Package\Shared",
                @"_Package\Shared\Folder",
                @"Package\",
                @"_Package\",
            };

            inputs.Each(x => x.GetOrigin().ShouldEqual("Package"));
        }
    }
}