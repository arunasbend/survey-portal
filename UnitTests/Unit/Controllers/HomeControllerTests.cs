using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using SurveyPortal.Controllers;

namespace UnitTests.Unit.Controllers
{
    [TestFixture]
    public class HomeControllerTests
    {
        [Test]
        public void AllowsToFetchFrontend()
        {
            var controller = new HomeController();

            var response = controller.Index();

            Assert.That(response, Is.InstanceOf(typeof(VirtualFileResult)));
        }
    }
}