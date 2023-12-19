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

        public void Execute(string numberTransport)
        {
            transportRepository.DeleteByNumber(numberTransport);
        }
    }
}
