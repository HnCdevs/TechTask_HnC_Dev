using System.ComponentModel.DataAnnotations.Schema;

namespace TechnicalTask.Models
{
    [Table("Departments")]
    public class Department : IModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int OfferingId { get; set; }
    }
}
