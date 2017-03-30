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
    public class BusinessRepositoryTests : IDisposable
    {
        private readonly DbSet<Country> _countriesSet;
        private readonly DbSet<Business> _businessesSet;
        private readonly TtContext _context;
        private readonly BusinessRepository _repository;

        public BusinessRepositoryTests()
        {
            var businessesList = new List<Business>
            {
                new Business { Id = 1, Name = "test 1", CountryId = 2 },
                new Business { Id = 2, Name = "test 2", CountryId = 1 }
            }.AsQueryable();

            var countriesList = new List<Country>
            {
                new Country { Id = 1, Name = "test 1" },
                new Country { Id = 2, Name = "test 2" }
            }.AsQueryable();

            _businessesSet = Substitute.For<DbSet<Business>, IQueryable<Business>>();
            ((IQueryable<Business>)_businessesSet).Provider.Returns(businessesList.Provider);
            ((IQueryable<Business>)_businessesSet).Expression.Returns(businessesList.Expression);
            ((IQueryable<Business>)_businessesSet).ElementType.Returns(businessesList.ElementType);
            ((IQueryable<Business>)_businessesSet).GetEnumerator().Returns(businessesList.GetEnumerator());

            _countriesSet = Substitute.For<DbSet<Country>, IQueryable<Country>>();
            ((IQueryable<Country>)_countriesSet).Provider.Returns(countriesList.Provider);
            ((IQueryable<Country>)_countriesSet).Expression.Returns(countriesList.Expression);
            ((IQueryable<Country>)_countriesSet).ElementType.Returns(countriesList.ElementType);
            ((IQueryable<Country>)_countriesSet).GetEnumerator().Returns(countriesList.GetEnumerator());

            _context = Substitute.For<TtContext>();
            _context.Businesses.Returns(_businessesSet);
            _context.Countries.Returns(_countriesSet);

            _repository = new BusinessRepository(_context);
        }

        [Fact]
        public void IsValidParentIsNullTest()
        {
            Country expected = null;
            _context.Countries.Find(Arg.Any<int>()).Returns(expected);
            Assert.Equal(false, _repository.IsValid(new Business { CountryId = 3 }));
        }

        [Fact]
        public void IsValidGoodTest()
        {
            var expected = _countriesSet.ToArray()[0];
            _context.Countries.Find(Arg.Any<int>()).Returns(expected);

            Assert.Equal(true, _repository.IsValid(new Business { Name = "test 3", CountryId = 1 }));
        }

        [Fact]
        public void IsValidItemExists()
        {
            var expected = _countriesSet.ToArray()[0];
            _context.Countries.Find(Arg.Any<int>()).Returns(expected);

            Assert.Equal(false, _repository.IsValid(new Business { Name = "test 2", CountryId = 1 }));
        }

        public void Dispose()
        {
            _context.ClearSubstitute();
        }
    }
}
