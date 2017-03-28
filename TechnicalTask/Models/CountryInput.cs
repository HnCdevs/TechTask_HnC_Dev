using System.Collections.Generic;

namespace TechnicalTask.Models
{
    public class CountryInput
    {
        public int OrganizationId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }

        public Country ConvertToCountry(CountryInput convertibleData)
        {
            var country = new Country
            {
                Name = convertibleData.Name,
                Code = convertibleData.Code,
                OrganizationCountries = new List<OrganizationCountry>
                {
                    new OrganizationCountry { OrganizationId = convertibleData.OrganizationId }
                }
            };

            return country;
        }
    }
}
