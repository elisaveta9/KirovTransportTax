using KirovTransportTax.Application.Interfaces.Repositories;
using KirovTransportTax.Domain.Entities;

namespace KirovTransportTax.Application.Transports.Queries
{
    public class GetAllTransportsQuery
    {
        private readonly ITransportRepository transportRepository;

        public GetAllTransportsQuery(ITransportRepository transportRepository)
        {
            this.transportRepository = transportRepository;
        }

        public IEnumerable<Transport> Execute()
        {
            return transportRepository.GetAll().Result;
        }
    }
}
