using KirovTransportTax.Domain.Entities;

namespace KirovTransportTax.Application.Interfaces
{
    public interface ITransportModelRepository : IRepository<TransportModel>
    {
        Task Create(TransportModel entity);
        void Delete(TransportModel entity);
        void Update(TransportModel entity);
        void Update(string oldModelPK, TransportModel entity);

        Task<IEnumerable<TransportModel>> GetModel(string modelPK);
    }
}
