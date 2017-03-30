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
    public class OfferingRepositoryTests : IDisposable
    {
        private readonly DbSet<Family> _familiesSet;
        private readonly DbSet<Offering> _offeringsSet;
        private readonly TtContext _context;
        private readonly OfferingRepository _repository;

        public OfferingRepositoryTests()
        {
            var offeringsList = new List<Offering>
            {
                new Offering { Id = 1, Name = "test 1", FamilyId = 2,},
                new Offering { Id = 2, Name = "test 2", FamilyId = 1, Departments = new List<Department>()}
            }.AsQueryable();

            var familiesList = new List<Family>
            {
                new Family { Id = 1, Name = "test 1" },
                new Family { Id = 2, Name = "test 2" }
            }.AsQueryable();

            _offeringsSet = Substitute.For<DbSet<Offering>, IQueryable<Offering>>();
            ((IQueryable<Offering>)_offeringsSet).Provider.Returns(offeringsList.Provider);
            ((IQueryable<Offering>)_offeringsSet).Expression.Returns(offeringsList.Expression);
            ((IQueryable<Offering>)_offeringsSet).ElementType.Returns(offeringsList.ElementType);
            ((IQueryable<Offering>)_offeringsSet).GetEnumerator().Returns(offeringsList.GetEnumerator());

            _familiesSet = Substitute.For<DbSet<Family>, IQueryable<Family>>();
            ((IQueryable<Family>)_familiesSet).Provider.Returns(familiesList.Provider);
            ((IQueryable<Family>)_familiesSet).Expression.Returns(familiesList.Expression);
            ((IQueryable<Family>)_familiesSet).ElementType.Returns(familiesList.ElementType);
            ((IQueryable<Family>)_familiesSet).GetEnumerator().Returns(familiesList.GetEnumerator());

            _context = Substitute.For<TtContext>();
            _context.Offerings.Returns(_offeringsSet);
            _context.Families.Returns(_familiesSet);

            _repository = new OfferingRepository(_context);
        }

        [Fact]
        public void IsValidParentIsNullTest()
        {
            Family expected = null;
            _context.Families.Find(Arg.Any<int>()).Returns(expected);
            Assert.Equal(false, _repository.IsValid(new Offering { FamilyId = 3 }));
        }

        [Fact]
        public void IsValidGoodTest()
        {
            var expected = _familiesSet.ToArray()[0];
            _context.Families.Find(Arg.Any<int>()).Returns(expected);

            Assert.Equal(true, _repository.IsValid(new Offering { Name = "test 3", FamilyId = 1 }));
        }

        [Fact]
        public void IsValidItemExists()
        {
            var expected = _familiesSet.ToArray()[0];
            _context.Families.Find(Arg.Any<int>()).Returns(expected);

            Assert.Equal(false, _repository.IsValid(new Offering { Name = "test 2", FamilyId = 1 }));
        }

        public void Dispose()
        {
            _context.ClearSubstitute();
        }
    }
}
