using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechnicalTask.Models
{
    [Table("Families")]
    public class Family
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public int BusinessId { get; set; }

        //[NotMapped]
        public ICollection<Offering> Offerings { get; set; }

        public Family()
        {
            Offerings = new List<Offering>();
        }
    }
}
