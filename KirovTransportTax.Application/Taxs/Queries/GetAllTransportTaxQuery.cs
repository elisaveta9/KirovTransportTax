using KirovTransportTax.Application.Interfaces.Repositories;
using KirovTransportTax.Domain.Entities;

namespace KirovTransportTax.Application.Taxs.Queries
{
    public class GetAllTransportTaxQuery
    {
        private readonly ITransportTaxRepository transportTaxRepository;

        public GetAllTransportTaxQuery(ITransportTaxRepository transportTaxRepository)
        {
            this.transportTaxRepository = transportTaxRepository;
        }

        public IEnumerable<TransportTax> Execute() =>
            transportTaxRepository.GetAll().Result;
    }
}
