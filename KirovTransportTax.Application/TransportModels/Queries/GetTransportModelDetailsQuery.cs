using KirovTransportTax.Application.Interfaces.Repositories;
using KirovTransportTax.Domain.Entities;

namespace KirovTransportTax.Application.TransportModels.Queries
{
    public class GetTransportModelDetailsQuery
    {
        private readonly ITransportModelRepository _transportModelRepository;

        public GetTransportModelDetailsQuery(ITransportModelRepository transportModelRepository)
        {
            _transportModelRepository = transportModelRepository;
        }

        public TransportModel Execute(string model)
        {
            var transportModel = _transportModelRepository.GetModel(model).Result ??
                throw new Exception($"{nameof(TransportModel)}: doesn't exsist transport model with name: {model}");
            return transportModel;
        }
    }
}
