using AutoMapper;
using KirovTransportTax.Application.Interfaces.Repositories;
using KirovTransportTax.Domain.Entities;

namespace KirovTransportTax.Application.Drivers.Commands
{
    public class DeleteDriverCommand
    {
        private readonly IDriverRepository _driverRepository;

        public DeleteDriverCommand(IDriverRepository driverRepository)
        {
            _driverRepository = driverRepository;
        }

        public bool Execute(Driver driver)
        {
            return _driverRepository.Delete(driver).Result != 0;
        }
    }
}
