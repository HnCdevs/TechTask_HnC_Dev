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
    public class DepartmentsControllerTests : IDisposable
    {
        private readonly DepartmentService _service;
        private readonly DepartmentsController _controller;

        public DepartmentsControllerTests()
        {
            var list = new List<Department>
            {
                new Department { Id = 1, Name = "test 1", OfferingId = 2 },
                new Department { Id = 2, Name = "test 2", OfferingId = 1 }

            }.AsQueryable();

            var mockContext = Substitute.For<TtContext>();
            var departmentRepository = Substitute.For<Repository<Department>>(mockContext);
            var offeringRepository = Substitute.For<Repository<Offering>>(mockContext);
            _service = Substitute.For<DepartmentService>(departmentRepository, offeringRepository);
            _service.GetList().Returns(list);
            _service.GetItem(Arg.Any<int>()).Returns(new Department { Id = 1, Name = "test 1", OfferingId = 1 });
            _service.Create(Arg.Any<Department>());
            _service.Update(Arg.Any<int>(), Arg.Any<Department>());
            _service.Delete(Arg.Any<int>());

            var mockLogger = Substitute.For<ILoggerFactory>();
            _controller = new DepartmentsController(_service, mockLogger);
        }

        [Fact]
        public void GetAllTest()
        {
            var departments = _controller.Get();
            Assert.Equal(2, departments.Count());
        }

        [Fact]
        public void GetItemTest()
        {
            var department = _controller.Get(1);
            Assert.Equal(1, department.Id);
        }

        [Fact]
        public void CreateBadInputTest()
        {
            Assert.Throws<ArgumentNullException>(() => _controller.Post(null));
        }

        [Fact]
        public void CreateGoodTest()
        {
            _service.IsValid(Arg.Any<Department>()).Returns(true);
            _controller.Post(new Department());
            _service.Received(1).Create(Arg.Any<Department>());
        }

        [Fact]
        public void CreateInvalidTest()
        {
            _service.IsValid(Arg.Any<Department>()).Returns(false);
            Assert.Throws<ArgumentException>(() => _controller.Post(new Department()));
        }

        [Fact]
        public void UpdateBadInputTest()
        {
            Assert.Throws<ArgumentNullException>(() => _controller.Put(0, null));
        }

        [Fact]
        public void UpdateGoodTest()
        {
            _service.IsValid(Arg.Any<Department>()).Returns(true);
            _controller.Put(0, new Department());
            _service.Received(1).Update(Arg.Any<int>(), Arg.Any<Department>());
        }

        [Fact]
        public void UpdateInvalidTest()
        {
            _service.IsValid(Arg.Any<Department>()).Returns(false);
            Assert.Throws<ArgumentException>(() => _controller.Put(0, new Department()));
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
