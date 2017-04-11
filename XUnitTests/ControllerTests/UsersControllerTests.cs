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
    public class UsersControllerTests : IDisposable
    {
        private readonly UserService _service;
        private readonly UsersController _controller;

        public UsersControllerTests()
        {
            var list = new List<User>
            {
                new User { Id = 1, Name = "test 1" },
                new User { Id = 2, Name = "test 2" }

            }.AsQueryable();

            var mockContext = Substitute.For<TtContext>();
            var userRepository = Substitute.For<Repository<User>>(mockContext);
            _service = Substitute.For<UserService>(userRepository);
            _service.GetList().Returns(list);
            _service.GetItem(Arg.Any<int>()).Returns(new User { Id = 1, Name = "test 1" });
            _service.Create(Arg.Any<User>());
            _service.Update(Arg.Any<int>(), Arg.Any<User>());
            _service.Delete(Arg.Any<int>());

            var mockLogger = Substitute.For<ILoggerFactory>();
            _controller = new UsersController(_service, mockLogger);
        }

        [Fact]
        public void GetAllTest()
        {
            var users = _controller.Get();
            Assert.Equal(2, users.Count());
        }

        [Fact]
        public void GetItemTest()
        {
            var user = _controller.Get(1);
            Assert.Equal(1, user.Id);
        }

        [Fact]
        public void CreateBadInputTest()
        {
            Assert.Throws<ArgumentNullException>(() => _controller.Post(null));
        }

        [Fact]
        public void CreateGoodTest()
        {
            _service.IsValid(Arg.Any<User>()).Returns(true);
            _controller.Post(new User());
            _service.Received(1).Create(Arg.Any<User>());
        }

        [Fact]
        public void UpdateBadInputTest()
        {
            Assert.Throws<ArgumentNullException>(() => _controller.Put(0, null));
        }

        [Fact]
        public void UpdateGoodTest()
        {
            _service.IsValid(Arg.Any<User>()).Returns(true);
            _controller.Put(0, new User());
            _service.Received(1).Update(Arg.Any<int>(), Arg.Any<User>());
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
