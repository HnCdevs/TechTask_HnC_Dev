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
    public class OrganizationsControllerTests : IDisposable
    {
        private readonly OrganizationRepository _repository;
        private readonly OrganizationsController _controller;

        public OrganizationsControllerTests()
        {
            var list = new List<Organization>
            {
                new Organization { Id = 1, Name = "test 1" },
                new Organization { Id = 2, Name = "test 2" }

            }.AsQueryable();

            var mockContext = Substitute.For<TtContext>();
            _repository = Substitute.For<OrganizationRepository>(mockContext);
            _repository.GetList().Returns(list);
            _repository.GetItem(Arg.Any<int>()).Returns(new Organization { Id = 1, Name = "test 1" });
            _repository.Create(Arg.Any<Organization>());
            _repository.Update(Arg.Any<int>(), Arg.Any<Organization>());
            _repository.Delete(Arg.Any<int>());

            _controller = new OrganizationsController(_repository);
        }

        [Fact]
        public void GetAllTest()
        {
            var organizations = _controller.Get();
            Assert.Equal(2, organizations.Count());
        }

        [Fact]
        public void GetItemTest()
        {
            var organization = _controller.Get(1);
            Assert.Equal(1, organization.Id);
        }

        [Fact]
        public void CreateBadInputTest()
        {
            Assert.Throws<ArgumentNullException>(() => _controller.Post(null));
        }

        [Fact]
        public void CreateGoodTest()
        {
            _repository.IsValid(Arg.Any<Organization>()).Returns(true);
            _controller.Post(new Organization());
            _repository.Received(1).Create(Arg.Any<Organization>());
        }

        [Fact]
        public void CreateInvalidTest()
        {
            _repository.IsValid(Arg.Any<Organization>()).Returns(false);
            Assert.Throws<ArgumentException>(() => _controller.Post(new Organization()));
        }

        [Fact]
        public void UpdateBadInputTest()
        {
            Assert.Throws<ArgumentNullException>(() => _controller.Put(0, null));
        }

        [Fact]
        public void UpdateGoodTest()
        {
            _repository.IsValid(Arg.Any<Organization>()).Returns(true);
            _controller.Put(0, new Organization());
            _repository.Received(1).Update(Arg.Any<int>(), Arg.Any<Organization>());
        }

        [Fact]
        public void UpdateInvalidTest()
        {
            _repository.IsValid(Arg.Any<Organization>()).Returns(false);
            Assert.Throws<ArgumentException>(() => _controller.Put(0, new Organization()));
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
