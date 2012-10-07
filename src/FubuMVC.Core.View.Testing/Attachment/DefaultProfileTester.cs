using FubuMVC.Core.Runtime.Conditionals;
using FubuMVC.Core.View.Attachment;
using NUnit.Framework;
using Rhino.Mocks;
using FubuTestingSupport;

namespace FubuMVC.Core.View.Testing.Attachment
{
    [TestFixture]
    public class DefaultProfileTester
    {
        [Test]
        public void condition_type_is_Always()
        {
            new DefaultProfile().ConditionType.ShouldEqual(typeof (Always));
        }

        [Test]
        public void filter_just_returns_the_same()
        {
            var views = new IViewToken[]{
                MockRepository.GenerateMock<IViewToken>(),
                MockRepository.GenerateMock<IViewToken>(),
                MockRepository.GenerateMock<IViewToken>(),
                MockRepository.GenerateMock<IViewToken>(),
                MockRepository.GenerateMock<IViewToken>(),
                MockRepository.GenerateMock<IViewToken>()
            };

            var original = new ViewBag(views);

            new DefaultProfile().Filter(original).ShouldBeTheSameAs(original);
        }
    }
}