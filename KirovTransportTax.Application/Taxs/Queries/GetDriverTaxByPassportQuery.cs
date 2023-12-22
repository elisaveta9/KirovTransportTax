using KirovTransportTax.Application.Interfaces.Repositories;
using KirovTransportTax.Domain.Entities;

namespace KirovTransportTax.Application.Taxs.Queries
{
    public class GetDriverTaxByPassportQuery
    {
        private readonly IDriverTaxRepository driverTaxRepository;

        public GetDriverTaxByPassportQuery(IDriverTaxRepository driverTaxRepository)
        {
            this.driverTaxRepository = driverTaxRepository;
        }

        public DriverTax? Execute(string passport) =>
            driverTaxRepository.GetByPassport(passport).Result;
    }
}
