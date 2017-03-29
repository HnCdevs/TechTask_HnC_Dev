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
        private DbSet<Department> _mDeptSet;
        private TtContext _mTtContext;
        private DepartmentRepository _repository;

        public DepartmentRepositoryTests()
        {
            var departments = new List<Department>
            {
                new Department { Id = 1, Name = "DeptTest", OfferingId = 1}
            }.AsQueryable();

            _mDeptSet = Substitute.For<DbSet<Department>, IQueryable<Department>>();
            ((IQueryable<Department>)_mDeptSet).Provider.Returns(departments.Provider);
            ((IQueryable<Department>)_mDeptSet).Expression.Returns(departments.Expression);
            ((IQueryable<Department>)_mDeptSet).ElementType.Returns(departments.ElementType);
            ((IQueryable<Department>)_mDeptSet).GetEnumerator().Returns(departments.GetEnumerator());

            _mTtContext = Substitute.For<TtContext>();
            _mTtContext.Departments.Returns(_mDeptSet);
        }

        [Fact]
        public void CreateDepartmentTest()
        {
            var expected = new Offering() {Name = "BbB"};
            _mTtContext.Offerings.Find(Arg.Any<int>()).Returns(expected);
            
            _repository = new DepartmentRepository(_mTtContext);
            _repository.Create(new Department { Name = "DeptTest", OfferingId = 1 });

            _mTtContext.Received(1).SaveChanges();
        }

        [Fact]
        public void CreateDepartmentNoOfferingsTest()
        {

            Offering expected = null;
            _mTtContext.Offerings.Find(Arg.Any<int>()).Returns(expected);

            _repository = new DepartmentRepository(_mTtContext);
            Assert.Throws<Exception>(() => _repository.Create(new Department { Name = "DeptTest", OfferingId = 1 }));
        }


        [Fact]
        public void CreateDepartmentHasDepartmentTest()
        {
            var departments = new List<Department>
            {
                new Department { Id = 1, Name = "DeptTest", OfferingId = 1}
            }.AsQueryable();


            _mDeptSet = Substitute.For<DbSet<Department>, IQueryable<Department>>();
            ((IQueryable<Department>)_mDeptSet).Provider.Returns(departments.Provider);
            ((IQueryable<Department>)_mDeptSet).Expression.Returns(departments.Expression);
            ((IQueryable<Department>)_mDeptSet).ElementType.Returns(departments.ElementType);
            ((IQueryable<Department>)_mDeptSet).GetEnumerator().Returns(departments.GetEnumerator());

            _mTtContext = Substitute.For<TtContext>();
            _mTtContext.Departments.Returns(_mDeptSet);


            var expected = new Offering() { Name = "BbB", Id = 1 };
            _mTtContext.Offerings.Find(Arg.Any<int>()).Returns(expected);

            _repository = new DepartmentRepository(_mTtContext);
            Assert.Throws<Exception>(() => _repository.Create(new Department { Name = "DeptTest", OfferingId = 1 }));
        }

    }
}
