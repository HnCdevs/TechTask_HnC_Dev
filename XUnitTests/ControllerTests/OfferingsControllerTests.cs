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
using Xunit;

namespace XUnitTests.ControllerTests
{
    public class OfferingsControllerTests : IDisposable
    {
        private readonly OfferingRepository _repository;
        private readonly OfferingsController _controller;

        public OfferingsControllerTests()
        {
            var list = new List<Offering>
            {
                new Offering { Id = 1, Name = "test 1", FamilyId = 2 },
                new Offering { Id = 2, Name = "test 2", FamilyId = 1 }

            }.AsQueryable();

            var mockContext = Substitute.For<TtContext>();
            _repository = Substitute.For<OfferingRepository>(mockContext);
            _repository.GetList().Returns(list);
            _repository.GetItem(Arg.Any<int>()).Returns(new Offering { Id = 1, Name = "test 1", FamilyId = 1 });
            _repository.Create(Arg.Any<Offering>());
            _repository.Update(Arg.Any<int>(), Arg.Any<Offering>());
            _repository.Delete(Arg.Any<int>());

            var mockLogger = Substitute.For<ILoggerFactory>();
            _controller = new OfferingsController(_repository, mockLogger);
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
            _repository.IsValid(Arg.Any<Offering>()).Returns(true);
            _controller.Post(new Offering());
            _repository.Received(1).Create(Arg.Any<Offering>());
        }

        [Fact]
        public void CreateInvalidTest()
        {
            _repository.IsValid(Arg.Any<Offering>()).Returns(false);
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
            _repository.IsValid(Arg.Any<Offering>()).Returns(true);
            _controller.Put(0, new Offering());
            _repository.Received(1).Update(Arg.Any<int>(), Arg.Any<Offering>());
        }

        [Fact]
        public void UpdateInvalidTest()
        {
            _repository.IsValid(Arg.Any<Offering>()).Returns(false);
            Assert.Throws<ArgumentException>(() => _controller.Put(0, new Offering()));
        }

        [Fact]
        public void DeleteTest()
        {
            _controller.Delete(0);
            _repository.Received(1).Delete(Arg.Any<int>());
        }

        public void Dispose()
        {
            _repository.ClearSubstitute();
        }
    }
}
