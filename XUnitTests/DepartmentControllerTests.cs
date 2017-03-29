using System;
using System.Collections.Generic;
using System.Linq;
using NSubstitute;
using TechnicalTask.Controllers;
using TechnicalTask.Data;
using TechnicalTask.Models;
using TechnicalTask.Repository;
using Xunit;

namespace XUnitTests
{
    public class DepartmentControllerTests : IClassFixture<RepoFixture>
    {
        private readonly DepartmentRepository _repository;
        //private readonly DepartmentRepository _mockRepository;
        private readonly DepartmentsController _controller;

        public DepartmentControllerTests(RepoFixture fixture)
        {
            _repository = fixture.Repository;          
            _controller = new DepartmentsController(_repository);
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
        public void CreateInvalidDepartmentTest()
        {
            _repository.IsValid(Arg.Any<Department>()).Returns(false);
            Assert.Throws<ArgumentException>(() => _controller.Post(new Department()));
        }

        [Fact]
        public void UpdateBadInputTest()
        {
            Assert.Throws<ArgumentNullException>(() => _controller.Put(Arg.Any<int>(), null));
        }

        [Fact]
        public void UpdateGoodTest()
        {
            _repository.IsValid(Arg.Any<Department>()).Returns(true);
            _controller.Put(Arg.Any<int>(), new Department());
            _repository.Received(1).Update(Arg.Any<int>(), Arg.Any<Department>());
        }

        [Fact]
        public void UpdateInvalidDepartmentTest()
        {
            _repository.IsValid(Arg.Any<Department>()).Returns(Arg.Is(false));
            Assert.Throws<ArgumentException>(() => _controller.Put(Arg.Any<int>(), new Department()));
        }

        //[Fact]
        //public void DeleteTest()
        //{
        //    _repository.Delete(Arg.Any<int>());
        //    _repository.Received(1).Delete(Arg.Any<int>());
        //}     
    }

    public class RepoFixture
    {
        public DepartmentRepository Repository { get; }

        public RepoFixture()
        {
            IQueryable<Department> departments = new List<Department>
            {
                new Department { Id = 1, Name = "test 1", OfferingId = 2 },
                new Department { Id =  2, Name = "test 2", OfferingId = 1 }

            }.AsQueryable();

            var mockContext = Substitute.For<TtContext>();
            Repository = Substitute.For<DepartmentRepository>(mockContext);
            Repository.GetList().Returns(departments);
            Repository.GetItem(Arg.Any<int>()).Returns(new Department{Id = 1, Name = "test 1", OfferingId = 1});
            Repository.Create(Arg.Any<Department>());
            Repository.Update(Arg.Any<int>(), Arg.Any<Department>());
            Repository.Delete(Arg.Any<int>());
        }
    }
}
