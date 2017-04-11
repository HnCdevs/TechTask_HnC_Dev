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
    public class FamilyServiceTests : IDisposable
    {
        private readonly Repository<Business> _businessRepository;
        private readonly Repository<Family> _familyRepository;
        private readonly FamilyService _service;

        public FamilyServiceTests()
        {
            var familiesList = new List<Family>
            {
                new Family { Id = 1, Name = "test 1", BusinessId = 2 },
                new Family { Id = 2, Name = "test 2", BusinessId = 1 },
                new Family { Id = 3, Name = "test 4", BusinessId = 1 }
            }.AsQueryable();

            var context = Substitute.For<TtContext>();

            _businessRepository = Substitute.For<Repository<Business>>(context);
            _familyRepository = Substitute.For<Repository<Family>>(context);

            _familyRepository.GetList().Returns(familiesList);

            _service = new FamilyService(_familyRepository, _businessRepository);
        }

        [Fact]
        public void IsValidParentIsNullTest()
        {
            Business expected = null;
            _businessRepository.GetItem(Arg.Any<int>()).Returns(expected);
            Assert.Equal(false, _service.IsValid(new Family { BusinessId = 3 }));
        }

        [Fact]
        public void IsValidGoodTest()
        {
            var expected = new Business { Id = 1 };
            _businessRepository.GetItem(Arg.Any<int>()).Returns(expected);

            Assert.Equal(true, _service.IsValid(new Family { Name = "test 3", BusinessId = 1 }));
        }

        [Fact]
        public void IsValidItemExists()
        {
            var expected = new Business { Id = 1 };
            _businessRepository.GetItem(Arg.Any<int>()).Returns(expected);

            Assert.Equal(false, _service.IsValid(new Family { Name = "test 2", BusinessId = 1 }));
        }

        public void Dispose()
        {
            _businessRepository.ClearSubstitute();
            _familyRepository.ClearSubstitute();
        }
    }
}
