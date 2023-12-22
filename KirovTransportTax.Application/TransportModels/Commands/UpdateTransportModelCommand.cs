using KirovTransportTax.Application.Interfaces.Repositories;
using KirovTransportTax.Domain.Entities;

namespace KirovTransportTax.Application.TransportModels.Commands
{
    public class UpdateTransportModelCommand
    {
        private readonly ITransportModelRepository _transportModelRepository;

        public UpdateTransportModelCommand(ITransportModelRepository transportModelRepository)
        {
            _transportModelRepository = transportModelRepository;
        }

        public bool Execute(TransportModel transportModel)
        {
            return _transportModelRepository.Update(transportModel).Result != 0;
        }

        public bool Execute(string oldModelName, TransportModel transportModel)
        {
            return _transportModelRepository.Update(oldModelName, transportModel).Result != 0;
        }
    }
}
