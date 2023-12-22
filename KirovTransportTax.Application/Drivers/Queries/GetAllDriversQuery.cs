using KirovTransportTax.Application.Interfaces.Repositories;
using KirovTransportTax.Domain.Entities;

namespace KirovTransportTax.Application.Drivers.Queries
{
    public class GetAllDriversQuery
    {
        private readonly IDriverRepository _driverRepository;

        public GetAllDriversQuery(IDriverRepository driverRepository)
        {
            _driverRepository = driverRepository;
        }

        public IEnumerable<Driver> Execute() =>
            _driverRepository.GetAll().Result.ToList();        
    }
}
