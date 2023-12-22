using KirovTransportTax.Application.Interfaces.Repositories;

namespace KirovTransportTax.Application.Drivers.Commands
{
    public class DeleteDriverByPassportCommand
    {
        private readonly IDriverRepository _driverRepository;

        public DeleteDriverByPassportCommand(IDriverRepository driverRepository)
        {
            _driverRepository = driverRepository;
        }

        public bool Execute(string passport)
        {
            return _driverRepository.DeleteByPassport(passport).Result != 0;
        }
    }
}
