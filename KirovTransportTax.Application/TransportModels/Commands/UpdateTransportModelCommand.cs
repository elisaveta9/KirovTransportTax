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

        public void Execute(TransportModel transportModel)
        {
            _transportModelRepository.Update(transportModel);
        }

        public void Execute(string oldModelName, TransportModel transportModel)
        {
            _transportModelRepository.Update(oldModelName, transportModel);
        }
    }
}
