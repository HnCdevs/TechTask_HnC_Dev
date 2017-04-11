using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechnicalTask.Models
{
    [Table("Countries")]
    public class Country : IModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }

        public ICollection<OrganizationCountry> OrganizationCountries { get; set; }
        //[NotMapped]
        public ICollection<Business> Businesses { get; set; }

        public Country()
        {
            OrganizationCountries = new List<OrganizationCountry>();
            Businesses = new List<Business>();
        }
    }
}
