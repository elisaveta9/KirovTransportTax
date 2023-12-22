using KirovTransportTax.Application.Interfaces.Repositories;
using KirovTransportTax.Domain.Entities;

namespace KirovTransportTax.Application.Taxs.Queries
{
    public class GetAllDriverTaxQuery
    {
        private readonly IDriverTaxRepository driverTaxRepository;

        public GetAllDriverTaxQuery(IDriverTaxRepository driverTaxRepository)
        {
            this.driverTaxRepository = driverTaxRepository;
        }

        public IEnumerable<DriverTax> Execute() =>
            driverTaxRepository.GetAll().Result;
    }
}
