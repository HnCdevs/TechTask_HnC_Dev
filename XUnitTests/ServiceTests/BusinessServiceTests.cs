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
    public class BusinessServiceTests : IDisposable
    {
        private readonly Repository<Country> _countryRepository;
        private readonly Repository<Business> _businessRepository;
        private readonly BusinessService _service;

        public BusinessServiceTests()
        {
            var businessesList = new List<Business>
            {
                new Business { Id = 1, Name = "test 1", CountryId = 2 },
                new Business { Id = 2, Name = "test 2", CountryId = 1 },
                new Business { Id = 3, Name = "test 4", CountryId = 1 }
            }.AsQueryable();

            var context = Substitute.For<TtContext>();

            _countryRepository = Substitute.For<Repository<Country>>(context);
            _businessRepository = Substitute.For<Repository<Business>>(context);

            _businessRepository.GetList().Returns(businessesList);
            _businessRepository.GetItem(Arg.Any<int>()).Returns(new Business { Id = 1, Name = "test 1", CountryId = 1 });

            _service = new BusinessService(_businessRepository, _countryRepository);
        }

        [Fact]
        public void IsValidParentIsNullTest()
        {
            Country expected = null;
            _countryRepository.GetItem(Arg.Any<int>()).Returns(expected);
            Assert.Equal(false, _service.IsValid(new Business { CountryId = 3 }));
        }

        [Fact]
        public void IsValidGoodTest()
        {
            var expected = new Country { Id = 1 };
            _countryRepository.GetItem(Arg.Any<int>()).Returns(expected);

            Assert.Equal(true, _service.IsValid(new Business { Name = "test 3", CountryId = 1 }));
        }

        [Fact]
        public void IsValidItemExists()
        {
            var expected = new Country { Id = 1 };
            _countryRepository.GetItem(Arg.Any<int>()).Returns(expected);

            Assert.Equal(false, _service.IsValid(new Business { Name = "test 2", CountryId = 1 }));
        }

        [Fact]
        public void GetListTest()
        {
            Assert.Equal(3, _service.GetList().Count());
        }

        [Fact]
        public void GetItemTest()
        {
            var business = _service.GetItem(1);
            Assert.Equal(1, business.Id);
        }

        public void Dispose()
        {
            _countryRepository.ClearSubstitute();
            _businessRepository.ClearSubstitute();
        }
    }
}
