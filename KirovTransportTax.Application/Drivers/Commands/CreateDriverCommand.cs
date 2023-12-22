using KirovTransportTax.Application.Interfaces.Repositories;
using KirovTransportTax.Domain.Entities;

namespace KirovTransportTax.Application.Drivers.Commands
{
    public class CreateDriverCommand
    {
        private readonly IDriverRepository _driverRepository;

        public CreateDriverCommand(IDriverRepository driverRepository)
        {
            _driverRepository = driverRepository;
        }

        public bool Execute(Driver driver)
        {
            return _driverRepository.Create(driver).Result != 0;
        }
    }
}
