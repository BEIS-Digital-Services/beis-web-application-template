using Beis.Common.Services;
using Beis.WebApplication.Models;
using Newtonsoft.Json;
using Moq;
using Microsoft.AspNetCore.Http;
using System.Text;
using HttpContextMoq;

namespace Beis.HelpToGrow.Voucher.Web.Tests.ApplyForDiscount.Services
{
    [TestFixture]
    public class SessionServiceTests
    {
        private SessionService _sut;

        [SetUp]
        public void Setup()
        {
            _sut = new SessionService();
        }

        [Test]
        public void Set()
        {
            var dto = new WebApplicationDto();
            var mockHttpContext = new Mock<HttpContext>();
            var mockSession = new Mock<ISession>();

            mockHttpContext
                .Setup(_ => _.Session)
                .Returns(() => mockSession.Object);

            _sut.Set("fake key", dto, mockHttpContext.Object);

            mockSession.Verify(_ => _.Set("fake key", It.IsAny<byte[]>()), Times.Once);
        }

        [Test]
        public void GetFails()
        {
            var fakeDto = new WebApplicationDto
            {
                
            };
            var fakeJson = JsonConvert.SerializeObject(fakeDto);
            var value = Encoding.UTF8.GetBytes(fakeJson);
            var mockHttpContext = new Mock<HttpContext>();
            var mockSession = new Mock<ISession>();

            mockHttpContext
                .Setup(_ => _.Session)
                .Returns(mockSession.Object);

            mockSession
                .Setup(_ => _.TryGetValue("fake key", out value))
                .Returns(false);

            var dto = _sut.Get<WebApplicationDto>("fake key", mockHttpContext.Object);

            Assert.Null(dto);
        }

        [Test]
        public void Get()
        {
            var fakeDto = new WebApplicationDto
            {
                
            };
            var fakeJson = JsonConvert.SerializeObject(fakeDto);
            var value = Encoding.UTF8.GetBytes(fakeJson);
            var mockHttpContext = new Mock<HttpContext>();
            var mockSession = new Mock<ISession>();

            mockHttpContext
                .Setup(_ => _.Session)
                .Returns(mockSession.Object);

            mockSession
                .Setup(_ => _.TryGetValue("fake key", out value))
                .Returns(true);

            var dto = _sut.Get<WebApplicationDto>("fake key", mockHttpContext.Object);

            Assert.NotNull(dto);
        }

        [Test]
        public void HasNoValidSession()
        {
            var mockHttpContext = new Mock<HttpContext>();

            mockHttpContext
                .Setup(_ => _.Session)
                .Returns(new SessionMock());

            var result = _sut.HasValidSession(mockHttpContext.Object);

            Assert.False(result);
        }

        [Test]
        public void HasValidSession()
        {
            var mockHttpContext = new Mock<HttpContext>();
            var mockSession = new Mock<ISession>();

            mockSession
                .Setup(_ => _.IsAvailable)
                .Returns(true);

            mockSession
                .Setup(_ => _.Keys)
                .Returns(new List<string> {"fake key"});

            mockHttpContext
                .Setup(_ => _.Session)
                .Returns(mockSession.Object);

            var result = _sut.HasValidSession(mockHttpContext.Object);

            Assert.That(result);
        }

        [Test]
        public void Remove()
        {
            var mockHttpContext = new Mock<HttpContext>();
            var mockSession = new Mock<ISession>();

            mockHttpContext
                .Setup(_ => _.Session)
                .Returns(mockSession.Object);

            _sut.Remove("fake key", mockHttpContext.Object);
        }
    }
}