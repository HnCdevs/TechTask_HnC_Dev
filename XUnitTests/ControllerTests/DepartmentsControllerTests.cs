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
    public class DepartmentsControllerTests : IDisposable
    {
        private readonly DepartmentRepository _repository;
        private readonly DepartmentsController _controller;

        public DepartmentsControllerTests()
        {
            var list = new List<Department>
            {
                new Department { Id = 1, Name = "test 1", OfferingId = 2 },
                new Department { Id = 2, Name = "test 2", OfferingId = 1 }

            }.AsQueryable();

            var mockContext = Substitute.For<TtContext>();
            _repository = Substitute.For<DepartmentRepository>(mockContext);
            _repository.GetList().Returns(list);
            _repository.GetItem(Arg.Any<int>()).Returns(new Department { Id = 1, Name = "test 1", OfferingId = 1 });
            _repository.Create(Arg.Any<Department>());
            _repository.Update(Arg.Any<int>(), Arg.Any<Department>());
            _repository.Delete(Arg.Any<int>());

            var mockLogger = Substitute.For<ILoggerFactory>();
            _controller = new DepartmentsController(_repository, mockLogger);
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
            _repository.IsValid(Arg.Any<Department>()).Returns(true);
            _controller.Post(new Department());
            _repository.Received(1).Create(Arg.Any<Department>());
        }

        [Fact]
        public void CreateInvalidTest()
        {
            _repository.IsValid(Arg.Any<Department>()).Returns(false);
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
            _repository.IsValid(Arg.Any<Department>()).Returns(true);
            _controller.Put(0, new Department());
            _repository.Received(1).Update(Arg.Any<int>(), Arg.Any<Department>());
        }

        [Fact]
        public void UpdateInvalidTest()
        {
            _repository.IsValid(Arg.Any<Department>()).Returns(false);
            Assert.Throws<ArgumentException>(() => _controller.Put(0, new Department()));
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
