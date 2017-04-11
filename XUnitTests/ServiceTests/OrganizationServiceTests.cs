﻿using System;
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
    public class OrganizationServiceTests : IDisposable
    {
        //private readonly DbSet<Organization> _organizationsSet;
        private readonly TtContext _context;
        private readonly OrganizationRepository _repository;

        public OrganizationServiceTests()
        {
            var organizationsList = new List<Organization>
            {
                new Organization { Id = 1, Name = "test 1" },
                new Organization { Id = 2, Name = "test 2" }
            }.AsQueryable();

            var organizationsSet = Substitute.For<DbSet<Organization>, IQueryable<Organization>>();
            ((IQueryable<Organization>)organizationsSet).Provider.Returns(organizationsList.Provider);
            ((IQueryable<Organization>)organizationsSet).Expression.Returns(organizationsList.Expression);
            ((IQueryable<Organization>)organizationsSet).ElementType.Returns(organizationsList.ElementType);
            ((IQueryable<Organization>)organizationsSet).GetEnumerator().Returns(organizationsList.GetEnumerator());          

            _context = Substitute.For<TtContext>();
            _context.Organizations.Returns(organizationsSet);

            _repository = new OrganizationRepository(_context);
        }

        [Fact]
        public void GetListTests()
        {
            Assert.Equal(2, _repository.GetList().Count());
        }

        public void Dispose()
        {
            _context.ClearSubstitute();
        }
    }
}