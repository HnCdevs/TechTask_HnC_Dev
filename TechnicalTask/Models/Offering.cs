using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechnicalTask.Models
{
    [Table("Offerings")]
    public class Offering
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public int FamilyId { get; set; }

        public ICollection<Department> Departments { get; set; }

        public Offering()
        {
            Departments = new List<Department>();
        }
    }
}
