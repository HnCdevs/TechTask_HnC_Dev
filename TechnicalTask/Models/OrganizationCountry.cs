

namespace TechnicalTask.Models
{
    public class OrganizationCountry
    {
        public int OrganizationId { get; set; }
        public int CountryId { get; set; }

        public Organization Organization { get; set; }
        public Country Country { get; set; }
    }
}
