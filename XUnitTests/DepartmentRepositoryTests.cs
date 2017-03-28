using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using TechnicalTask.Data;
using TechnicalTask.Models;
using TechnicalTask.Repository;
using Xunit;

namespace XUnitTests
{
    public class DepartmentRepositoryTests
    {
        private DbSet<Department> _mockSet;
        private TtContext _mockContext;
        private DepartmentRepository _repository;

        public DepartmentRepositoryTests()
        {
            var departments = new List<Department>
            {
                new Department { Id = 1, Name = "BBB", OfferingId = 232}
            }.AsQueryable();


            _mockSet = Substitute.For<DbSet<Department>, IQueryable<Department>>();
            ((IQueryable<Department>)_mockSet).Provider.Returns(departments.Provider);
            ((IQueryable<Department>)_mockSet).Expression.Returns(departments.Expression);
            ((IQueryable<Department>)_mockSet).ElementType.Returns(departments.ElementType);
            ((IQueryable<Department>)_mockSet).GetEnumerator().Returns(departments.GetEnumerator());

            _mockContext = Substitute.For<TtContext>();
            _mockContext.Departments.Returns(_mockSet);

        }

        [Fact]
        public void CreateDepartmentTest()
        {

            var expected = new Offering() {Name = "BbB"};
            _mockContext.Offerings.Find(Arg.Any<int>()).Returns(expected);
            
            _repository = new DepartmentRepository(_mockContext);
            _repository.Create(new Department { Name = "DeptTest", OfferingId = 1 });

            _mockContext.Received(1).SaveChanges();
        }

        [Fact]
        public void CreateDepartmentNoOfferingsTest()
        {

            Offering expected = null;
            _mockContext.Offerings.Find(Arg.Any<int>()).Returns(expected);

            _repository = new DepartmentRepository(_mockContext);
            Assert.Throws<Exception>(() => _repository.Create(new Department { Name = "DeptTest", OfferingId = 1 }));
        }


        [Fact]
        public void CreateDepartmentHasDepartmentTest()
        {
            var departments = new List<Department>
            {
                new Department { Id = 1, Name = "DeptTest", OfferingId = 1}
            }.AsQueryable();


            _mockSet = Substitute.For<DbSet<Department>, IQueryable<Department>>();
            ((IQueryable<Department>)_mockSet).Provider.Returns(departments.Provider);
            ((IQueryable<Department>)_mockSet).Expression.Returns(departments.Expression);
            ((IQueryable<Department>)_mockSet).ElementType.Returns(departments.ElementType);
            ((IQueryable<Department>)_mockSet).GetEnumerator().Returns(departments.GetEnumerator());

            _mockContext = Substitute.For<TtContext>();
            _mockContext.Departments.Returns(_mockSet);


            var expected = new Offering() { Name = "BbB", Id = 1 };
            _mockContext.Offerings.Find(Arg.Any<int>()).Returns(expected);

            _repository = new DepartmentRepository(_mockContext);
            Assert.Throws<Exception>(() => _repository.Create(new Department { Name = "DeptTest", OfferingId = 1 }));
        }

    }
}
