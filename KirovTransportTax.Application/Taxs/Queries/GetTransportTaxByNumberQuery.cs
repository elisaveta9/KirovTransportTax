using KirovTransportTax.Application.Interfaces.Repositories;
using KirovTransportTax.Domain.Entities;

namespace KirovTransportTax.Application.Taxs.Queries
{
    public class GetTransportTaxByNumberQuery
    {
        private readonly ITransportTaxRepository transportTaxRepository;

        public GetTransportTaxByNumberQuery(ITransportTaxRepository transportTaxRepository)
        {
            this.transportTaxRepository = transportTaxRepository;
        }

        public TransportTax? Execute(string number) =>
            transportTaxRepository.GetByNumber(number).Result;
    }
}
