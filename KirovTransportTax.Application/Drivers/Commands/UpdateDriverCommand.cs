using AutoMapper;
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

        public bool Execute(Driver driver)
        {
            return _driverRepository.Delete(driver).Result != 0;
        }

        public bool Execute(string oldPssport, Driver driver)
        {
            return _driverRepository.Update(oldPssport, driver).Result != 0;
        }
    }
}
