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
    public class OfferingServiceTests : IDisposable
    {
        private readonly Repository<Family> _familyRepository;
        private readonly Repository<Offering> _offeringRepository;
        private readonly OfferingService _service;

        public OfferingServiceTests()
        {
            var offeringsList = new List<Offering>
            {
                new Offering { Id = 1, Name = "test 1", FamilyId = 2 },
                new Offering { Id = 2, Name = "test 2", FamilyId = 1 },
                new Offering { Id = 3, Name = "test 4", FamilyId = 1 }
            }.AsQueryable();

            var context = Substitute.For<TtContext>();

            _familyRepository = Substitute.For<Repository<Family>>(context);
            _offeringRepository = Substitute.For<Repository<Offering>>(context);

            _offeringRepository.GetList().Returns(offeringsList);

            _service = new OfferingService(_offeringRepository, _familyRepository);
        }

        [Fact]
        public void IsValidParentIsNullTest()
        {
            Family expected = null;
            _familyRepository.GetItem(Arg.Any<int>()).Returns(expected);
            Assert.Equal(false, _service.IsValid(new Offering { FamilyId = 3 }));
        }

        [Fact]
        public void IsValidGoodTest()
        {
            var expected = new Family { Id = 1 };
            _familyRepository.GetItem(Arg.Any<int>()).Returns(expected);

            Assert.Equal(true, _service.IsValid(new Offering { Name = "test 3", FamilyId = 1 }));
        }

        [Fact]
        public void IsValidItemExists()
        {
            var expected = new Family { Id = 1 };
            _familyRepository.GetItem(Arg.Any<int>()).Returns(expected);

            Assert.Equal(false, _service.IsValid(new Offering { Name = "test 2", FamilyId = 1 }));
        }

        public void Dispose()
        {
            _familyRepository.ClearSubstitute();
            _offeringRepository.ClearSubstitute();
        }
    }
}
