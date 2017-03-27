using System.Collections.Generic;

namespace TechnicalTask.Models
{
    public class CountryInput
    {
        public int OrganizationId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }

        public Country ConvertToCountry(CountryInput convertibleInput)
        {
            var country = new Country
            {
                Name = convertibleInput.Name,
                Code = convertibleInput.Code,
                OrganizationCountries = new List<OrganizationCountry>
                {
                    new OrganizationCountry {OrganizationId = convertibleInput.OrganizationId}
                }
            };

            return country;
        }
    }
}
