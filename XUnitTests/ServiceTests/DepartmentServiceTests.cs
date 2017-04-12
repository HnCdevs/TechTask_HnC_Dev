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
    public class DepartmentServiceTests : IDisposable
    {
        private readonly Repository<Offering> _offeringRepository;
        private readonly Repository<Department> _departmentRepository;
        private readonly DepartmentService _service;

        public DepartmentServiceTests()
        {
            var departmentsList = new List<Department>
            {
                new Department { Id = 1, Name = "test 1", OfferingId = 2 },
                new Department { Id = 2, Name = "test 2", OfferingId = 1 },
                new Department { Id = 3, Name = "test 4", OfferingId = 1 }
            }.AsQueryable();

            var context = Substitute.For<TtContext>();

            _offeringRepository = Substitute.For<Repository<Offering>>(context);
            _departmentRepository = Substitute.For<Repository<Department>>(context);

            _departmentRepository.GetList().Returns(departmentsList);
            _departmentRepository.GetItem(Arg.Any<int>()).Returns(new Department {Id = 1, Name = "test 1", OfferingId = 1});
            _departmentRepository.Create(Arg.Any<Department>());
            _departmentRepository.Update(Arg.Any<Department>(), Arg.Any<Department>());
            _departmentRepository.Delete(Arg.Any<Department>());

            _service = new DepartmentService(_departmentRepository, _offeringRepository);
        }

        [Fact]
        public void IsValidParentIsNullTest()
        {
            Offering expected = null;
            _offeringRepository.GetItem(Arg.Any<int>()).Returns(expected);
            Assert.Equal(false, _service.IsValid(new Department { OfferingId = 3 }));
        }

        [Fact]
        public void IsValidGoodTest()
        {
            var expected = new Offering { Id = 1 };
            _offeringRepository.GetItem(Arg.Any<int>()).Returns(expected);

            Assert.Equal(true, _service.IsValid(new Department { Name = "test 3", OfferingId = 1 }));
        }

        [Fact]
        public void IsValidItemExists()
        {
            var expected = new Offering { Id = 1 };
            _offeringRepository.GetItem(Arg.Any<int>()).Returns(expected);

            Assert.Equal(false, _service.IsValid(new Department { Name = "test 2", OfferingId = 1 }));
        }

        [Fact]
        public void GetListTest()
        {
            Assert.Equal(3, _service.GetList().Count());
        }

        [Fact]
        public void GetItemTest()
        {
            var department = _service.GetItem(1);
            Assert.Equal(1, department.Id);
        }

        [Fact]
        public void CreateTest()
        {
            var item = new Department();
            _service.Create(item);
            _departmentRepository.Received(1).Create(item);
        }

        [Fact]
        public void UpdateTest()
        {
            var entry = _departmentRepository.GetItem(Arg.Any<int>());
            var item = new Department();
            _service.Update(0, item);
            _departmentRepository.Received(1).Update(entry, item);
        }

        [Fact]
        public void DeleteTest()
        {
            var item = new Department();
            _service.Delete(0);
            _departmentRepository.Received(1).Delete(item);
        }

        public void Dispose()
        {
            _offeringRepository.ClearSubstitute();
            _departmentRepository.ClearSubstitute();
        }
    }
}
