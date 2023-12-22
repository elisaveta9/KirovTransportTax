using KirovTransportTax.Application.Interfaces.Repositories;
using KirovTransportTax.Domain.Entities;

namespace KirovTransportTax.Application.Drivers.Commands
{
    public class UpdateDriverCommand
    {
        private readonly IDriverRepository _driverRepository;

        public UpdateDriverCommand(IDriverRepository driverRepository)
        {
            _driverRepository = driverRepository;
        }

        public void Execute(Driver driver)
        {
            _driverRepository.Delete(driver);
        }

        public void Execute(string oldPssport, Driver driver)
        {
            _driverRepository.Update(oldPssport, driver);
        }
    }
}
