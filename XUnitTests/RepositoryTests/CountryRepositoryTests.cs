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
    public class CountryRepositoryTests : IDisposable
    {
        private readonly DbSet<Organization> _organizationsSet;
        private readonly DbSet<OrganizationCountry> _organizationCountriesSet;
        private readonly TtContext _context;
        private readonly CountryRepository _repository;

        public CountryRepositoryTests()
        {
            var organizationCountriesList = new List<OrganizationCountry>
            {
                new OrganizationCountry { Id = 1, OrganizationId = 1, CountryId = 1, Country = new Country { Name = "test 2" }},
                new OrganizationCountry { Id = 2, OrganizationId = 2, CountryId = 2, Country = new Country { Name = "test 1" }}
            }.AsQueryable();

            var organizationsList = new List<Organization>
            {
                new Organization { Id = 1, Name = "test 1", OrganizationCountries = new List<OrganizationCountry>(organizationCountriesList) },
                new Organization { Id = 2, Name = "test 2", OrganizationCountries = new List<OrganizationCountry>(organizationCountriesList) }
            }.AsQueryable();            

            _organizationCountriesSet = Substitute.For<DbSet<OrganizationCountry>, IQueryable<OrganizationCountry>>();
            ((IQueryable<OrganizationCountry>)_organizationCountriesSet).Provider.Returns(organizationCountriesList.Provider);
            ((IQueryable<OrganizationCountry>)_organizationCountriesSet).Expression.Returns(organizationCountriesList.Expression);
            ((IQueryable<OrganizationCountry>)_organizationCountriesSet).ElementType.Returns(organizationCountriesList.ElementType);
            ((IQueryable<OrganizationCountry>)_organizationCountriesSet).GetEnumerator().Returns(organizationCountriesList.GetEnumerator());

            _organizationsSet = Substitute.For<DbSet<Organization>, IQueryable<Organization>>();
            ((IQueryable<Organization>)_organizationsSet).Provider.Returns(organizationsList.Provider);
            ((IQueryable<Organization>)_organizationsSet).Expression.Returns(organizationsList.Expression);
            ((IQueryable<Organization>)_organizationsSet).ElementType.Returns(organizationsList.ElementType);
            ((IQueryable<Organization>)_organizationsSet).GetEnumerator().Returns(organizationsList.GetEnumerator());
            
            _context = Substitute.For<TtContext>();
            _context.OrganizationCountries.Returns(_organizationCountriesSet);
            _context.Organizations.Returns(_organizationsSet);

            _repository = new CountryRepository(_context);
        }

        [Fact]
        public void IsValidParentIsNullTest()
        {
            Organization expected = null;
            _context.Organizations.Find(Arg.Any<int>()).Returns(expected);
            Assert.Equal(false, _repository.IsValid(new Country { OrganizationCountries = new List<OrganizationCountry>
            {
                new OrganizationCountry { OrganizationId = 3 }
            }}));
        }

        [Fact]
        public void IsValidGoodTest()
         {
            var expected = _organizationsSet.ToArray()[0];
            _context.Organizations.Find(Arg.Any<int>()).Returns(expected);

            Assert.Equal(true, _repository.IsValid(new Country
            {
                Name = "test 3",
                OrganizationCountries = new List<OrganizationCountry>
                {
                    new OrganizationCountry { OrganizationId = 1 }
                }
            }));
        }

        [Fact]
        public void IsValidItemExists()
        {
            var expected = _organizationsSet.ToArray()[0];
            _context.Organizations.Find(Arg.Any<int>()).Returns(expected);

            Assert.Equal(false, _repository.IsValid(new Country
            {
                Name = "test 2",
                OrganizationCountries = new List<OrganizationCountry>
                {
                    new OrganizationCountry { OrganizationId = 1 }
                }
            }));
        }

        [Fact]
        public void CreateTest()
        {
            var country = new Country { Id = 1, OrganizationCountries = _organizationCountriesSet.ToList() };
            //var expected = _organizationsSet.ToArray()[0];
            //_context.Organizations.Find(Arg.Any<int>()).Returns(expected);

            _repository.Create(country);
            _context.Received(2).SaveChanges();
        }

        public void Dispose()
        {
            _context.ClearSubstitute();
        }
    }
}
