using Microsoft.EntityFrameworkCore;
using NSubstitute;
using TechnicalTask.Data;
using TechnicalTask.Models;
using TechnicalTask.Repository;
using Xunit;

namespace XUnitTests
{
    public class DepartmentRepositoryTests
    {
        private DbSet<Department>_mockSet;
        private TtContext _mockContext;
        private DepartmentRepository _repository;

        public DepartmentRepositoryTests()
        {
            
        }

        [Fact]
        public void CreateDepartmentTest()
        {
            _mockSet = Substitute.For<DbSet<Department>>();
            _mockContext = Substitute.For<TtContext>();
            _mockContext.Departments.Returns(_mockSet);
            _repository = new DepartmentRepository(_mockContext);

            _repository.Create(new Department {Name = "DeptTest", OfferingId = 1});
            _mockSet.Received(1).Add(Arg.Any<Department>());
            _mockContext.Received(1).SaveChanges();
        }
    }
}
