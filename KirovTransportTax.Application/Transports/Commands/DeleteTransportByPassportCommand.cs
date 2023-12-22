using KirovTransportTax.Application.Interfaces.Repositories;

namespace KirovTransportTax.Application.Transports.Commands
{
    public class DeleteTransportByPassportCommand
    {
        private readonly ITransportRepository transportRepository;

        public DeleteTransportByPassportCommand(ITransportRepository transportRepository)
        {
            this.transportRepository = transportRepository;
        }

        public bool Execute(string passport)
        {
            return transportRepository.DeleteByPassport(passport).Result != 0;
        }
    }
}
