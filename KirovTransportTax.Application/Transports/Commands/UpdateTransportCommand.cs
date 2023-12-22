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

        public bool Execute(Transport transport) 
        {
            return transportRepository.Update(transport).Result != 0;
        }

        public bool Execute(string oldTransportNumber, Transport transport)
        {
            return transportRepository.Update(oldTransportNumber, transport).Result != 0;
        }
    }
}
