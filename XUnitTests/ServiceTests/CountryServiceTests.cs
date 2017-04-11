using System;
using System.Collections.Generic;
using System.Linq;
using NSubstitute;
using NSubstitute.ClearExtensions;
using TechnicalTask.Data;
using TechnicalTask.Models;
using TechnicalTask.Repository;
using TechnicalTask.Services;
using Xunit;

namespace XUnitTests.ServiceTests
{
    public class CountryServiceTests : IDisposable
    {
        private readonly Repository<Organization> _organizationRepository;
        private readonly Repository<OrganizationCountry> _organizationCountryRepository;
        private readonly Repository<Country> _countryRepository;
        private readonly CountryService _service;

        public CountryServiceTests()
        {
            var organizationCountriesList = new List<OrganizationCountry>
            {
                new OrganizationCountry { Id = 1, OrganizationId = 1, CountryId = 1, Country = new Country { Name = "test 2" }},
                new OrganizationCountry { Id = 2, OrganizationId = 2, CountryId = 2, Country = new Country { Name = "test 1" }},
                new OrganizationCountry { Id = 3, OrganizationId = 1, CountryId = 3, Country = new Country { Name = "test 4" }}
            }.AsQueryable();
            
            var context = Substitute.For<TtContext>();

            _organizationRepository = Substitute.For<Repository<Organization>>(context);
            _organizationCountryRepository = Substitute.For<Repository<OrganizationCountry>>(context);
            _countryRepository = Substitute.For<Repository<Country>>(context);

            _organizationCountryRepository.GetList().Returns(organizationCountriesList);

            _service = new CountryService(_countryRepository, _organizationCountryRepository, _organizationRepository);
        }

        [Fact]
        public void IsValidParentIsNullTest()
        {
            Organization expected = null;
            _organizationRepository.GetItem(Arg.Any<int>()).Returns(expected);
            Assert.Equal(false, _service.IsValid(new Country
            {
                OrganizationCountries = new List<OrganizationCountry>
            {
                new OrganizationCountry { OrganizationId = 3 }
            }
            }));
        }

        [Fact]
        public void IsValidGoodTest()
         {
            var expected = new Organization { Id = 1 };
            _organizationRepository.GetItem(Arg.Any<int>()).Returns(expected);

            Assert.Equal(true, _service.IsValid(new Country
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
            var expected = new Organization { Id = 1 };
            _organizationRepository.GetItem(Arg.Any<int>()).Returns(expected);

            Assert.Equal(false, _service.IsValid(new Country
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
            var country = new Country { Id = 1, OrganizationCountries = { new OrganizationCountry { OrganizationId = 1 }}};

            _service.Create(country);
            _countryRepository.Received(1).Create(country);
        }

        public void Dispose()
        {
            _organizationCountryRepository.ClearSubstitute();
            _countryRepository.ClearSubstitute();
        }
    }
}
