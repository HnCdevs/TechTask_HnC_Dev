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
    public class OrganizationServiceTests : IDisposable
    {
        private readonly Repository<Organization> _repository;
        private readonly OrganizationService _service;

        public OrganizationServiceTests()
        {
            var organizationsList = new List<Organization>
            {
                new Organization { Id = 1, Name = "test 1" },
                new Organization { Id = 2, Name = "test 2" }
            }.AsQueryable();

            var context = Substitute.For<TtContext>();
            _repository = Substitute.For<Repository<Organization>>(context);

            _repository.GetList().Returns(organizationsList);

            _service = new OrganizationService(_repository);
        }

        [Fact]
        public void GetTreeTests()
        {
            Assert.Equal(2, _service.GetTree().Count());
        }

        public void Dispose()
        {
            _repository.ClearSubstitute();
        }
    }
}
