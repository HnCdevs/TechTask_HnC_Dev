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
    public class BusinessesControllerTests : IDisposable
    {
        private readonly BusinessService _service;
        private readonly BusinessesController _controller;

        public BusinessesControllerTests()
        {
            var list = new List<Business>
            {
                new Business { Id = 1, Name = "test 1", CountryId = 2 },
                new Business { Id = 2, Name = "test 2", CountryId = 1 }

            }.AsQueryable();

            var mockContext = Substitute.For<TtContext>();
            var businessRepository = Substitute.For<Repository<Business>>(mockContext);
            var countryRepository = Substitute.For<Repository<Country>>(mockContext);
            _service = Substitute.For<BusinessService>(businessRepository, countryRepository);
            _service.GetList().Returns(list);
            _service.GetItem(Arg.Any<int>()).Returns(new Business { Id = 1, Name = "test 1", CountryId = 1 });
            _service.Create(Arg.Any<Business>());
            _service.Update(Arg.Any<int>(), Arg.Any<Business>());
            _service.Delete(Arg.Any<int>());

            var mockLogger = Substitute.For<ILoggerFactory>();
            _controller = new BusinessesController(_service, mockLogger);
        }

        [Fact]
        public void GetAllTest()
        {
            var businesses = _controller.Get();
            Assert.Equal(2, businesses.Count());
        }

        [Fact]
        public void GetItemTest()
        {
            var business = _controller.Get(1);
            Assert.Equal(1, business.Id);
        }

        [Fact]
        public void CreateBadInputTest()
        {
            Assert.Throws<ArgumentNullException>(() => _controller.Post(null));
        }

        [Fact]
        public void CreateGoodTest()
        {
            _service.IsValid(Arg.Any<Business>()).Returns(true);
            _controller.Post(new Business());
            _service.Received(1).Create(Arg.Any<Business>());
        }

        [Fact]
        public void CreateInvalidTest()
        {
            _service.IsValid(Arg.Any<Business>()).Returns(false);
            Assert.Throws<ArgumentException>(() => _controller.Post(new Business()));
        }

        [Fact]
        public void UpdateBadInputTest()
        {
            Assert.Throws<ArgumentNullException>(() => _controller.Put(0, null));
        }

        [Fact]
        public void UpdateGoodTest()
        {
            _service.IsValid(Arg.Any<Business>()).Returns(true);
            _controller.Put(0, new Business());
            _service.Received(1).Update(Arg.Any<int>(), Arg.Any<Business>());
        }

        [Fact]
        public void UpdateInvalidTest()
        {
            _service.IsValid(Arg.Any<Business>()).Returns(false);
            Assert.Throws<ArgumentException>(() => _controller.Put(0, new Business()));
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
