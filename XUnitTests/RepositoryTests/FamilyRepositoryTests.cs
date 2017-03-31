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
    public class FamilyRepositoryTests : IDisposable
    {
        private readonly DbSet<Business> _businessesSet;
        private readonly DbSet<Family> _familiesSet;
        private readonly TtContext _context;
        private readonly FamilyRepository _repository;

        public FamilyRepositoryTests()
        {
            var familiesList = new List<Family>
            {
                new Family { Id = 1, Name = "test 1", BusinessId = 2 },
                new Family { Id = 2, Name = "test 2", BusinessId = 1 }
            }.AsQueryable();

            var businessesList = new List<Business>
            {
                new Business { Id = 1, Name = "test 1" },
                new Business { Id = 2, Name = "test 2" }
            }.AsQueryable();

            _familiesSet = Substitute.For<DbSet<Family>, IQueryable<Family>>();
            ((IQueryable<Family>)_familiesSet).Provider.Returns(familiesList.Provider);
            ((IQueryable<Family>)_familiesSet).Expression.Returns(familiesList.Expression);
            ((IQueryable<Family>)_familiesSet).ElementType.Returns(familiesList.ElementType);
            ((IQueryable<Family>)_familiesSet).GetEnumerator().Returns(familiesList.GetEnumerator());

            _businessesSet = Substitute.For<DbSet<Business>, IQueryable<Business>>();
            ((IQueryable<Business>)_businessesSet).Provider.Returns(businessesList.Provider);
            ((IQueryable<Business>)_businessesSet).Expression.Returns(businessesList.Expression);
            ((IQueryable<Business>)_businessesSet).ElementType.Returns(businessesList.ElementType);
            ((IQueryable<Business>)_businessesSet).GetEnumerator().Returns(businessesList.GetEnumerator());

            _context = Substitute.For<TtContext>();
            _context.Families.Returns(_familiesSet);
            _context.Businesses.Returns(_businessesSet);

            _repository = new FamilyRepository(_context);
        }

        [Fact]
        public void IsValidParentIsNullTest()
        {
            Business expected = null;
            _context.Businesses.Find(Arg.Any<int>()).Returns(expected);
            Assert.Equal(false, _repository.IsValid(new Family { BusinessId = 3 }));
        }

        [Fact]
        public void IsValidGoodTest()
        {
            var expected = _businessesSet.ToArray()[0];
            _context.Businesses.Find(Arg.Any<int>()).Returns(expected);

            Assert.Equal(true, _repository.IsValid(new Family { Name = "test 3", BusinessId = 1 }));
        }

        [Fact]
        public void IsValidItemExists()
        {
            var expected = _businessesSet.ToArray()[0];
            _context.Businesses.Find(Arg.Any<int>()).Returns(expected);

            Assert.Equal(false, _repository.IsValid(new Family { Name = "test 2", BusinessId = 1 }));
        }

        public void Dispose()
        {
            _context.ClearSubstitute();
        }
    }
}
