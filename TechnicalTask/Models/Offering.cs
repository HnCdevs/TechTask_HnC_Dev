using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechnicalTask.Models
{
    [Table("Offerings")]
    public class Offering : IModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int FamilyId { get; set; }

        //[NotMapped]
        public ICollection<Department> Departments { get; set; }

        public Offering()
        {
            Departments = new List<Department>();
        }
    }
}
