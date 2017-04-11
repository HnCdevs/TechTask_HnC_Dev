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
    public class OfferingsControllerTests : IDisposable
    {
        private readonly OfferingService _service;
        private readonly OfferingsController _controller;

        public OfferingsControllerTests()
        {
            var list = new List<Offering>
            {
                new Offering { Id = 1, Name = "test 1", FamilyId = 2 },
                new Offering { Id = 2, Name = "test 2", FamilyId = 1 }

            }.AsQueryable();

            var mockContext = Substitute.For<TtContext>();
            var offeringRepository = Substitute.For<Repository<Offering>>(mockContext);
            var familyRepository = Substitute.For<Repository<Family>>(mockContext);
            _service = Substitute.For<OfferingService>(offeringRepository, familyRepository);
            _service.GetList().Returns(list);
            _service.GetItem(Arg.Any<int>()).Returns(new Offering { Id = 1, Name = "test 1", FamilyId = 1 });
            _service.Create(Arg.Any<Offering>());
            _service.Update(Arg.Any<int>(), Arg.Any<Offering>());
            _service.Delete(Arg.Any<int>());

            var mockLogger = Substitute.For<ILoggerFactory>();
            _controller = new OfferingsController(_service, mockLogger);
        }

        [Fact]
        public void GetAllTest()
        {
            var offerings = _controller.Get();
            Assert.Equal(2, offerings.Count());
        }

        [Fact]
        public void GetItemTest()
        {
            var offering = _controller.Get(1);
            Assert.Equal(1, offering.Id);
        }

        [Fact]
        public void CreateBadInputTest()
        {
            Assert.Throws<ArgumentNullException>(() => _controller.Post(null));
        }

        [Fact]
        public void CreateGoodTest()
        {
            _service.IsValid(Arg.Any<Offering>()).Returns(true);
            _controller.Post(new Offering());
            _service.Received(1).Create(Arg.Any<Offering>());
        }

        [Fact]
        public void CreateInvalidTest()
        {
            _service.IsValid(Arg.Any<Offering>()).Returns(false);
            Assert.Throws<ArgumentException>(() => _controller.Post(new Offering()));
        }

        [Fact]
        public void UpdateBadInputTest()
        {
            Assert.Throws<ArgumentNullException>(() => _controller.Put(0, null));
        }

        [Fact]
        public void UpdateGoodTest()
        {
            _service.IsValid(Arg.Any<Offering>()).Returns(true);
            _controller.Put(0, new Offering());
            _service.Received(1).Update(Arg.Any<int>(), Arg.Any<Offering>());
        }

        [Fact]
        public void UpdateInvalidTest()
        {
            _service.IsValid(Arg.Any<Offering>()).Returns(false);
            Assert.Throws<ArgumentException>(() => _controller.Put(0, new Offering()));
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
