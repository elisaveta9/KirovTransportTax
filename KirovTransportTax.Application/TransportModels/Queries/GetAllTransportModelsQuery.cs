using KirovTransportTax.Application.Interfaces.Repositories;
using KirovTransportTax.Domain.Entities;

namespace KirovTransportTax.Application.TransportModels.Queries
{
    public class GetAllTransportModelsQuery
    {
        private readonly ITransportModelRepository _transportModelRepository;

        public GetAllTransportModelsQuery(ITransportModelRepository transportModelRepository)
        {
            _transportModelRepository = transportModelRepository;
        }

        public IEnumerable<TransportModel> Execute()
        {
            return _transportModelRepository.GetAll().Result;
        }
    }
}
