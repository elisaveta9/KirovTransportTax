using KirovTransportTax.Application.Interfaces.Repositories;

namespace KirovTransportTax.Application.TransportModels.Commands
{
    public class DeleteTransportModelByModelCommand
    {
        private readonly ITransportModelRepository _transportModelRepository;

        public DeleteTransportModelByModelCommand(ITransportModelRepository transportModelRepository)
        {
            _transportModelRepository = transportModelRepository;
        }

        public bool Execute(string model)
        {
            return _transportModelRepository.DeleteByModel(model).Result != 0;
        }
    }
}
