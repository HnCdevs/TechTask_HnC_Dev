using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using NSubstitute.ClearExtensions;
using TechnicalTask.Data;
using TechnicalTask.Models;
using TechnicalTask.Repository;
using Xunit;

namespace XUnitTests.RepositoryTests
{
    public class DepartmentRepositoryTests : IDisposable
    {
        private readonly DbSet<Offering> _offeringsSet;
        private readonly DbSet<Department> _departmentsSet;
        private readonly TtContext _context;
        private readonly DepartmentRepository _repository;

        public DepartmentRepositoryTests()
        {
            var departmentsList = new List<Department>
            {
                new Department { Id = 1, Name = "test 1", OfferingId = 2 },
                new Department { Id = 2, Name = "test 2", OfferingId = 1 }
            }.AsQueryable();

            var offeringsList = new List<Offering>
            {
                new Offering { Id = 1, Name = "test 1" },
                new Offering { Id = 2, Name = "test 2" }
            }.AsQueryable();
            
            _departmentsSet = Substitute.For<DbSet<Department>, IQueryable<Department>>();
            ((IQueryable<Department>)_departmentsSet).Provider.Returns(departmentsList.Provider);
            ((IQueryable<Department>)_departmentsSet).Expression.Returns(departmentsList.Expression);
            ((IQueryable<Department>)_departmentsSet).ElementType.Returns(departmentsList.ElementType);
            ((IQueryable<Department>)_departmentsSet).GetEnumerator().Returns(departmentsList.GetEnumerator());

            _offeringsSet = Substitute.For<DbSet<Offering>, IQueryable<Offering>>();
            ((IQueryable<Offering>)_offeringsSet).Provider.Returns(offeringsList.Provider);
            ((IQueryable<Offering>)_offeringsSet).Expression.Returns(offeringsList.Expression);
            ((IQueryable<Offering>)_offeringsSet).ElementType.Returns(offeringsList.ElementType);
            ((IQueryable<Offering>)_offeringsSet).GetEnumerator().Returns(offeringsList.GetEnumerator());

            _context = Substitute.For<TtContext>();
            _context.Departments.Returns(_departmentsSet);
            _context.Offerings.Returns(_offeringsSet);

            _context.Set<Department>().Returns(_departmentsSet);            
            _repository = new DepartmentRepository(_context);
        }

        [Fact]
        public void IsValidParentIsNullTest()
        {
            Offering expected = null;
            _context.Offerings.Find(Arg.Any<int>()).Returns(expected);
            Assert.Equal(false, _repository.IsValid(new Department { OfferingId = 3 }));
        }

        [Fact]
        public void IsValidGoodTest()
        {
            var expected = _offeringsSet.ToArray()[0];
            _context.Offerings.Find(Arg.Any<int>()).Returns(expected);

            Assert.Equal(true, _repository.IsValid(new Department { Name = "test 3", OfferingId = 1 }));
        }

        [Fact]
        public void IsValidItemExists()
        {
            var expected = _offeringsSet.ToArray()[0];
            _context.Offerings.Find(Arg.Any<int>()).Returns(expected);

            Assert.Equal(false, _repository.IsValid(new Department { Name = "test 2", OfferingId = 1 }));
        }

        [Fact]
        public void GetListTest()
        {
            Assert.Equal(2, _repository.GetList().Count());
        }

        [Fact]
        public void CreateTest()
        {
            _repository.Create(new Department());
            _context.Received(1).SaveChanges();
        }

        [Fact]
        public void UpdateTest()
        {
            _repository.Update(0, new Department());
            _context.Received(1).SaveChanges();
        }

        public void Dispose()
        {
            _context.ClearSubstitute();
        }
    }
}
