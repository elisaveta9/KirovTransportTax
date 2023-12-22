using KirovTransportTax.Application.Interfaces.Repositories;
using KirovTransportTax.Domain.Entities;

namespace KirovTransportTax.Application.TransportModels.Commands
{
    public class DeleteTransportModelCommand
    {
        private readonly ITransportModelRepository _transportModelRepository;

        public DeleteTransportModelCommand(ITransportModelRepository transportModelRepository)
        {
            _transportModelRepository = transportModelRepository;
        }

        public bool Execute(TransportModel transportModel) 
        {
            return _transportModelRepository.Delete(transportModel).Result != 0;
        }
    }
}
