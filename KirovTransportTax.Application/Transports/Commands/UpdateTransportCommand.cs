using KirovTransportTax.Application.Interfaces.Repositories;
using KirovTransportTax.Domain.Entities;

namespace KirovTransportTax.Application.Transports.Commands
{
    public class UpdateTransportCommand
    {
        private readonly ITransportRepository transportRepository;

        public UpdateTransportCommand(ITransportRepository transportRepository)
        {
            this.transportRepository = transportRepository;
        }

        public void Execute(Transport transport) 
        {
            transportRepository.Update(transport);
        }

        public void Execute(string oldTransportNumber, Transport transport)
        {
            transportRepository.Update(oldTransportNumber, transport);
        }
    }
}
