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
    public class FamiliesControllerTests : IDisposable
    {
        private readonly FamilyRepository _repository;
        private readonly FamiliesController _controller;

        public FamiliesControllerTests()
        {
            var list = new List<Family>
            {
                new Family { Id = 1, Name = "test 1", BusinessId = 2 },
                new Family { Id = 2, Name = "test 2", BusinessId = 1 }

            }.AsQueryable();

            var mockContext = Substitute.For<TtContext>();
            _repository = Substitute.For<FamilyRepository>(mockContext);
            _repository.GetList().Returns(list);
            _repository.GetItem(Arg.Any<int>()).Returns(new Family { Id = 1, Name = "test 1", BusinessId = 1 });
            _repository.Create(Arg.Any<Family>());
            _repository.Update(Arg.Any<int>(), Arg.Any<Family>());
            _repository.Delete(Arg.Any<int>());

            var mockLogger = Substitute.For<ILoggerFactory>();
            _controller = new FamiliesController(_repository, mockLogger);
        }

        [Fact]
        public void GetAllTest()
        {
            var families = _controller.Get();
            Assert.Equal(2, families.Count());
        }

        [Fact]
        public void GetItemTest()
        {
            var family = _controller.Get(1);
            Assert.Equal(1, family.Id);
        }

        [Fact]
        public void CreateBadInputTest()
        {
            Assert.Throws<ArgumentNullException>(() => _controller.Post(null));
        }

        [Fact]
        public void CreateGoodTest()
        {
            _repository.IsValid(Arg.Any<Family>()).Returns(true);
            _controller.Post(new Family());
            _repository.Received(1).Create(Arg.Any<Family>());
        }

        [Fact]
        public void CreateInvalidTest()
        {
            _repository.IsValid(Arg.Any<Family>()).Returns(false);
            Assert.Throws<ArgumentException>(() => _controller.Post(new Family()));
        }

        [Fact]
        public void UpdateBadInputTest()
        {
            Assert.Throws<ArgumentNullException>(() => _controller.Put(0, null));
        }

        [Fact]
        public void UpdateGoodTest()
        {
            _repository.IsValid(Arg.Any<Family>()).Returns(true);
            _controller.Put(0, new Family());
            _repository.Received(1).Update(Arg.Any<int>(), Arg.Any<Family>());
        }

        [Fact]
        public void UpdateInvalidTest()
        {
            _repository.IsValid(Arg.Any<Family>()).Returns(false);
            Assert.Throws<ArgumentException>(() => _controller.Put(0, new Family()));
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
