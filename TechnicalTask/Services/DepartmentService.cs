using System.Linq;
using TechnicalTask.Models;
using TechnicalTask.Repository;

namespace TechnicalTask.Services
{
    public class DepartmentService : Service<Department>
    {
        private readonly IRepository<Department> _departmentRepository;
        private readonly IRepository<Offering> _offeringRepository;

        public DepartmentService(IRepository<Department> departmentRepository, IRepository<Offering> offeringRepository) : base(departmentRepository)
        {
            _departmentRepository = departmentRepository;
            _offeringRepository = offeringRepository;
        }        

        public override bool IsValid(Department item)
        {
            var offering = _offeringRepository.GetItem(item.OfferingId);

            if (offering == null) return false;

            var departments = _departmentRepository.GetList().Where(x => x.OfferingId == offering.Id).ToList();

            return departments.All(x => x.Name != item.Name);
        }      
    }
}
