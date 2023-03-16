
using Beis.WebApplication.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Beis.HelpToGrow.Voucher.Web.Tests.ApplyForDiscount
{
    [TestFixture]

    public class SessionExpiredControllerTest
    {
        private SessionExpiredController _sut;

        [SetUp]
        public void Setup()
        {
            _sut = new SessionExpiredController();
        }

        [Test]
        public void Index()
        {
            var viewResult = (ViewResult) _sut.Index();
            //var viewModel = viewResult.Model as SessionExpiredViewModel;

            Assert.NotNull(viewResult);
            

        }
    }
}