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

        public void Execute(Driver driver)
        {
            _driverRepository.Delete(driver);
        }
    }
}
