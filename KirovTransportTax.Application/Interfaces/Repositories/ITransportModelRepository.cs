using KirovTransportTax.Domain.Entities;

namespace KirovTransportTax.Application.Interfaces.Repositories
{
    public interface ITransportModelRepository : IRepository<TransportModel>
    {
        Task<int> Create(TransportModel entity);
        Task<int> Delete(TransportModel entity);
        Task<int> DeleteByModel(string modelPK);
        Task<int> Update(TransportModel entity);
        Task<int> Update(string oldModelPK, TransportModel entity);

        Task<TransportModel> GetModel(string modelPK);
    }
}
