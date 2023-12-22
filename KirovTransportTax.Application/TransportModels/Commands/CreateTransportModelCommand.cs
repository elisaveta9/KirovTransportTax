using KirovTransportTax.Application.Interfaces.Repositories;
using KirovTransportTax.Domain.Entities;

namespace KirovTransportTax.Application.TransportModels.Commands
{
    public class CreateTransportModelCommand
    {
        private readonly ITransportModelRepository _transportModelRepository;

        public CreateTransportModelCommand(ITransportModelRepository transportModelRepository)
        {
            _transportModelRepository = transportModelRepository;
        }

        public bool Execute(TransportModel transportModel)
        {
            return _transportModelRepository.Create(transportModel).Result != 0;
        }
    }
}
