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

        public void Execute(TransportModel transportModel) 
        {
            _transportModelRepository.Delete(transportModel);
        }
    }
}
