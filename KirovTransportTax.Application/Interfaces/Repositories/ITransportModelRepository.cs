using KirovTransportTax.Domain.Entities;

namespace KirovTransportTax.Application.Interfaces.Repositories
{
    public interface ITransportModelRepository : IRepository<TransportModel>
    {
        Task<int> Create(TransportModel entity);
        void Delete(TransportModel entity);
        void DeleteByModel(string modelPK);
        void Update(TransportModel entity);
        void Update(string oldModelPK, TransportModel entity);

        Task<TransportModel> GetModel(string modelPK);
    }
}
