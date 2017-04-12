using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechnicalTask.Models
{
    [Table("Businesses")]
    public class Business
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public int CountryId { get; set; }

        public ICollection<Family> Families { get; set; }

        public Business()
        {
            Families = new List<Family>();
        }
    }
}
