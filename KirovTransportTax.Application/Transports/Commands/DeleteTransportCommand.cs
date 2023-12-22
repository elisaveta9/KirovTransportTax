using KirovTransportTax.Application.Interfaces.Repositories;
using KirovTransportTax.Domain.Entities;

namespace KirovTransportTax.Application.Transports.Commands
{
    public class DeleteTransportCommand
    {
        private readonly ITransportRepository transportRepository;

        public DeleteTransportCommand(ITransportRepository transportRepository)
        {
            this.transportRepository = transportRepository;
        }

        public void Execute(Transport transport)
        {
            transportRepository.Delete(transport);
        }
    }
}
