using KirovTransportTax.Application.Interfaces.Repositories;

namespace KirovTransportTax.Application.Transports.Commands
{
    public class DeleteTransportByNumberCommand
    {
        private readonly ITransportRepository transportRepository;

        public DeleteTransportByNumberCommand(ITransportRepository transportRepository)
        {
            this.transportRepository = transportRepository;
        }

        public bool Execute(string numberTransport)
        {
            return transportRepository.DeleteByNumber(numberTransport).Result != 0;
        }
    }
}
