using KirovTransportTax.Application.Interfaces.Repositories;
using KirovTransportTax.Domain.Entities;

namespace KirovTransportTax.Application.Transports.Queries
{
    public class GetTransportDetailsQuery
    {
        private readonly ITransportRepository transportRepository;

        public GetTransportDetailsQuery(ITransportRepository transportRepository)
        {
            this.transportRepository = transportRepository;
        }

        public Transport Execute(string numberTransport)
        {
            var transport = transportRepository.GetByNumber(numberTransport).Result ??
                throw new Exception($"{nameof(Transport)}: doesn't exsist transport with number: {numberTransport}");
            return transport;
        }
    }
}
