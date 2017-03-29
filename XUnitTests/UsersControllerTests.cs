using System;
using System.Collections.Generic;
using System.Linq;
using NSubstitute;
using NSubstitute.ClearExtensions;
using TechnicalTask.Controllers;
using TechnicalTask.Data;
using TechnicalTask.Models;
using TechnicalTask.Repository;
using Xunit;

namespace XUnitTests
{
    public class UsersControllerTests : IDisposable
    {
        private readonly UserRepository _repository;
        private readonly UsersController _controller;

        public UsersControllerTests()
        {
            var list = new List<User>
            {
                new User { Id = 1, Name = "test 1" },
                new User { Id = 2, Name = "test 2" }

            }.AsQueryable();

            var mockContext = Substitute.For<TtContext>();
            _repository = Substitute.For<UserRepository>(mockContext);
            _repository.GetList().Returns(list);
            _repository.GetItem(Arg.Any<int>()).Returns(new User { Id = 1, Name = "test 1" });
            _repository.Create(Arg.Any<User>());
            _repository.Update(Arg.Any<int>(), Arg.Any<User>());
            _repository.Delete(Arg.Any<int>());

            _controller = new UsersController(_repository);
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
            _repository.IsValid(Arg.Any<User>()).Returns(true);
            _controller.Post(new User());
            _repository.Received(1).Create(Arg.Any<User>());
        }

        [Fact]
        public void UpdateBadInputTest()
        {
            Assert.Throws<ArgumentNullException>(() => _controller.Put(0, null));
        }

        [Fact]
        public void UpdateGoodTest()
        {
            _repository.IsValid(Arg.Any<User>()).Returns(true);
            _controller.Put(0, new User());
            _repository.Received(1).Update(Arg.Any<int>(), Arg.Any<User>());
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
