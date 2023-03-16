
using Beis.WebApplication.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Beis.HelpToGrow.Voucher.Web.Tests.Common
{
    internal class NoOpResult : IActionResult
    {
        public Task ExecuteResultAsync(ActionContext context)
        {
            return Task.CompletedTask;
        }
    }

    [TestFixture]
    public class NotDefaultAttributeTest
    {
        private NotDefaultAttribute _sut;

        [SetUp]
        public void Setup()
        {
            _sut = new NotDefaultAttribute();
        }

        [Test]
        public void IsNotValid()
        {
            const int zero = 0;

            Assert.False(_sut.IsValid(zero));
        }

        [Test]
        public void IsValidNull()
        {
            Assert.That(_sut.IsValid(null));
        }

        [Test]
        public void IsValidInt()
        {
            Assert.That(_sut.IsValid(3));
        }

        [Test]
        public void IsValidViewModel()
        {
            Assert.That(_sut.IsValid(new DemoViewModel()));
        }
    }

    internal class DemoViewModel
    {
       
            [Required]
            [NotDefault]
            [RegularExpression("[0-9]*$")]
            public int EmployeeNumbers { get; set; }
        
    }

}