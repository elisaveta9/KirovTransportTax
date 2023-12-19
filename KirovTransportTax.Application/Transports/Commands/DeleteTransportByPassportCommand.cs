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

        public void Execute(string passport)
        {
            transportRepository.DeleteByPassport(passport);
        }
    }
}
