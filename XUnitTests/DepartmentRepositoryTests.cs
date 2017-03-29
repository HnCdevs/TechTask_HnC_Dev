using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using NSubstitute.ClearExtensions;
using NSubstitute.Core.Arguments;
using TechnicalTask.Data;
using TechnicalTask.Models;
using TechnicalTask.Repository;
using Xunit;

namespace XUnitTests
{
    public class DepartmentRepositoryTests : IDisposable
    {
        private readonly Offering _offering;
        private readonly DbSet<Department> _departmentSet;
        private readonly TtContext _context;
        private readonly DepartmentRepository _repository;

        public DepartmentRepositoryTests()
        {
            var departmentList = new List<Department>
            {
                new Department { Id = 1, Name = "test 1", OfferingId = 2 },
                new Department { Id = 2, Name = "test 2", OfferingId = 1 }
            }.AsQueryable();

            _offering = new Offering { Id = 1, Name = "test", Departments = new List<Department>(departmentList)};

            _departmentSet = Substitute.For<DbSet<Department>, IQueryable<Department>>();
            ((IQueryable<Department>)_departmentSet).Provider.Returns(departmentList.Provider);
            ((IQueryable<Department>)_departmentSet).Expression.Returns(departmentList.Expression);
            ((IQueryable<Department>)_departmentSet).ElementType.Returns(departmentList.ElementType);
            ((IQueryable<Department>)_departmentSet).GetEnumerator().Returns(departmentList.GetEnumerator());

            _context = Substitute.For<TtContext>();
            _context.Departments.Returns(_departmentSet);
            //_context.Departments.All(x => x.Name != Arg.Any<string>()).Returns(Arg.Any<bool>());
            
            _context.SetAdded(Arg.Any<Department>());
            _context.SetValues(Arg.Any<Department>(), Arg.Any<Department>());

            _repository = new DepartmentRepository(_context);
        }

        [Fact]
        public void IsValidParentIsNullTest()
        {
            Offering offering = null;
            _context.Offerings.Find(Arg.Any<int>()).Returns(offering);
            Assert.Equal(false, _repository.IsValid(new Department()));
        }

        [Fact]
        public void IsValidGoodTest()
        {
                      
        }

        [Fact]
        public void IsValidItemExists()
        {
            var item = new Department { Id = 1, Name = "testName", OfferingId = 1 };
            _context.Offerings.Find(Arg.Any<int>()).Returns(new Offering());            

            Expression<Func<Department, bool>> qw = department => true;

            _context.Departments.All(qw).Returns(false);
            Assert.Equal(false, _repository.IsValid(item));
        }
        //[Fact]
        //public void CreateDepartmentTest()
        //{
        //    var expected = new Offering() {Name = "BbB"};
        //    //_mTtContext.Offerings.Find(Arg.Any<int>()).Returns(expected);
            
        //    //_repository = new DepartmentRepository(_mTtContext);
        //    _repository.Create(new Department { Name = "DeptTest", OfferingId = 1 });

        //    //_mTtContext.Received(1).SaveChanges();
        //}

        //[Fact]
        //public void CreateDepartmentNoOfferingsTest()
        //{

        //    Offering expected = null;
        //    //_mTtContext.Offerings.Find(Arg.Any<int>()).Returns(expected);

        //    //_repository = new DepartmentRepository(_mTtContext);
        //    Assert.Throws<Exception>(() => _repository.Create(new Department { Name = "DeptTest", OfferingId = 1 }));
        //}


        //[Fact]
        //public void CreateDepartmentHasDepartmentTest()
        //{
        //    var departments = new List<Department>
        //    {
        //        new Department { Id = 1, Name = "DeptTest", OfferingId = 1}
        //    }.AsQueryable();


        //    //_set = Substitute.For<DbSet<Department>, IQueryable<Department>>();
        //    ((IQueryable<Department>)_set).Provider.Returns(departments.Provider);
        //    ((IQueryable<Department>)_set).Expression.Returns(departments.Expression);
        //    ((IQueryable<Department>)_set).ElementType.Returns(departments.ElementType);
        //    ((IQueryable<Department>)_set).GetEnumerator().Returns(departments.GetEnumerator());

        //    //_mTtContext = Substitute.For<TtContext>();
        //    //_mTtContext.Departments.Returns(_set);


        //    var expected = new Offering() { Name = "BbB", Id = 1 };
        //    //_mTtContext.Offerings.Find(Arg.Any<int>()).Returns(expected);

        //    //_repository = new DepartmentRepository(_mTtContext);
        //    Assert.Throws<Exception>(() => _repository.Create(new Department { Name = "DeptTest", OfferingId = 1 }));
        //}

        public void Dispose()
        {
            _context.ClearSubstitute();
        }
    }
}
