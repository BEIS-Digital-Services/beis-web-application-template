
using Beis.Common.Interfaces;
using Beis.WebApplication.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using Moq;
using Beis.WebApplication.Controllers;
using Beis.Ebss.ApplicationPortal.Web.Controllers;
using Microsoft.AspNetCore.Routing;
using Beis.Common.Infrastructure;
using Beis.WebApplication.Models;

namespace Beis.HelpToGrow.Voucher.Web.Tests.Common
{
    [TestFixture]
    public class CookieFilterTest
    {
        private CookieFilter<WebApplicationDto> _sut;
        private Mock<ISessionService> _mockSessionService;

        [SetUp]
        public void Setup()
        {
            _mockSessionService = new Mock<ISessionService>();
            _sut = new CookieFilter<WebApplicationDto>(_mockSessionService.Object);
        }

        [Test]
        public void OnResultExecutingNotController()
        {
            HttpContext http = new DefaultHttpContext();
            var contactUsController = new CookiesController();
            var routeData = new RouteData();
            routeData.Values.Add("Action", "FakeAction");
            routeData.Values.Add("Controller", "FakeController");
            contactUsController.ControllerContext = new ControllerContext {RouteData = routeData};
            var executing = CreateResultExecutingContext(http, contactUsController);

            _sut.OnResultExecuting(executing);

            Assert.NotNull(contactUsController.ViewData["CookieBannerViewModel"]);
        }

        private static ActionContext CreateActionContext(HttpContext context) => new(context, new(), new());

        private static ResultExecutingContext CreateResultExecutingContext(HttpContext context, Controller controller) =>
            new(CreateActionContext(context), Array.Empty<IFilterMetadata>(), new NoOpResult(), controller);
    }
}