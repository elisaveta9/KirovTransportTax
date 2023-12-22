using KirovTransportTax.Application.Interfaces.Repositories;
using KirovTransportTax.Domain.Entities;

namespace KirovTransportTax.Application.Drivers.Queries
{
    public class GetDriverDetailsQuery
    {
        private readonly IDriverRepository _driverRepository;

        public GetDriverDetailsQuery(IDriverRepository driverRepository)
        {
            _driverRepository = driverRepository;
        }

        public Driver Execute(string passport)
        {
            var driver = _driverRepository.GetByPassport(passport).Result ?? 
                throw new Exception($"{nameof(Driver)}: doesn't exsist driver with passport: {passport}");
            return driver;
        }
    }
}
