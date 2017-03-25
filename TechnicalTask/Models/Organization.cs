using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechnicalTask.Models
{
    [Table("Organizations")]
    public class Organization
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public OrganizationType OrganizationType { get; set; }
        public string Owner { get; set; }

        public ICollection<OrganizationCountry> OrganizationCountries { get; set; }

        public Organization()
        {
            OrganizationCountries = new List<OrganizationCountry>();
        }
    }
}
