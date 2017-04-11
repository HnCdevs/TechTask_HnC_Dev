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
    public class OrganizationsControllerTests : IDisposable
    {
        private readonly OrganizationService _service;
        private readonly OrganizationsController _controller;

        public OrganizationsControllerTests()
        {
            var list = new List<Organization>
            {
                new Organization { Id = 1, Name = "test 1" },
                new Organization { Id = 2, Name = "test 2" }

            }.AsQueryable();

            var mockContext = Substitute.For<TtContext>();
            var organizationRepository = Substitute.For<Repository<Organization>>(mockContext);
            _service = Substitute.For<OrganizationService>(organizationRepository);
            _service.GetList().Returns(list);
            _service.GetItem(Arg.Any<int>()).Returns(new Organization { Id = 1, Name = "test 1" });
            _service.Create(Arg.Any<Organization>());
            _service.Update(Arg.Any<int>(), Arg.Any<Organization>());
            _service.Delete(Arg.Any<int>());

            var mockLogger = Substitute.For<ILoggerFactory>();
            _controller = new OrganizationsController(_service, mockLogger);
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
            _controller.Post(new Organization());
            _service.Received(1).Create(Arg.Any<Organization>());
        }

        [Fact]
        public void UpdateBadInputTest()
        {
            Assert.Throws<ArgumentNullException>(() => _controller.Put(0, null));
        }

        [Fact]
        public void UpdateGoodTest()
        {
            _controller.Put(0, new Organization());
            _service.Received(1).Update(Arg.Any<int>(), Arg.Any<Organization>());
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
