using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ClearExtensions;
using TechnicalTask.Controllers;
using TechnicalTask.Data;
using TechnicalTask.Models;
using TechnicalTask.Repository;
using TechnicalTask.Services;
using Xunit;

namespace XUnitTests.ControllerTests
{
    public class CountriesControllerTests : IDisposable
    {
        private readonly CountryService _service;
        private readonly CountriesController _controller;

        public CountriesControllerTests()
        {
            var list = new List<Country>
            {
                new Country { Id = 1, Name = "test 1" },
                new Country { Id = 2, Name = "test 2" }

            }.AsQueryable();

            var mockContext = Substitute.For<TtContext>();
            var countryRepository = Substitute.For<Repository<Country>>(mockContext);
            var organizationCountryRepository = Substitute.For<Repository<OrganizationCountry>>(mockContext);
            var organizationRepository = Substitute.For<Repository<Organization>>(mockContext);
            _service = Substitute.For<CountryService>(countryRepository, organizationCountryRepository, organizationRepository);
            _service.GetList().Returns(list);
            _service.GetItem(Arg.Any<int>()).Returns(new Country { Id = 1, Name = "test 1" });
            _service.Create(Arg.Any<Country>());
            _service.Update(Arg.Any<int>(), Arg.Any<Country>());
            _service.Delete(Arg.Any<int>());

            var mockLogger = Substitute.For<ILoggerFactory>();
            _controller = new CountriesController(_service, mockLogger);
        }

        [Fact]
        public void GetAllTest()
        {
            var countries = _controller.Get();
            Assert.Equal(2, countries.Count());
        }

        [Fact]
        public void GetItemTest()
        {
            var country = _controller.Get(1);
            Assert.Equal(1, country.Id);
        }

        [Fact]
        public void CreateBadInputTest()
        {
            Assert.Throws<ArgumentNullException>(() => _controller.Post(null));
        }

        [Fact]
        public void CreateGoodTest()
        {
            _service.IsValid(Arg.Any<Country>()).Returns(true);
            _controller.Post(new CountryInput());
            _service.Received(1).Create(Arg.Any<Country>());
        }

        [Fact]
        public void CreateInvalidTest()
        {
            _service.IsValid(Arg.Any<Country>()).Returns(false);
            Assert.Throws<ArgumentException>(() => _controller.Post(new CountryInput()));
        }

        [Fact]
        public void UpdateBadInputTest()
        {
            Assert.Throws<ArgumentNullException>(() => _controller.Put(0, null));
        }

        [Fact]
        public void UpdateGoodTest()
        {
            _service.IsValid(Arg.Any<Country>()).Returns(true);
            _controller.Put(0, new CountryInput());
            _service.Received(1).Update(Arg.Any<int>(), Arg.Any<Country>());
        }

        [Fact]
        public void UpdateInvalidTest()
        {
            _service.IsValid(Arg.Any<Country>()).Returns(false);
            Assert.Throws<ArgumentException>(() => _controller.Put(0, new CountryInput()));
        }

        [Fact]
        public void DeleteTest()
        {
            _controller.Delete(0);
            _service.Received(1).Delete(Arg.Any<int>());
        }

        public void Dispose()
        {
            _service.ClearSubstitute();
        }
    }
}
