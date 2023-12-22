using KirovTransportTax.Domain.Entities;

namespace KirovTransportTax.Application.Interfaces.Repositories
{
    public interface ITransportRepository : IRepository<Transport>
    {
        Task<int> Create(Transport entity);
        Task<int> Delete(Transport entity);
        Task<int> DeleteByNumber(string numberTransporPK);
        Task<int> DeleteByPassport(string passportFK);
        Task<int> Update(Transport entity);
        Task<int> Update(string oldNumberTransporPK, Transport entity);

        Task<Transport> GetByNumber(string numberTransporPK);
        Task<IEnumerable<Transport>> GetByPassport(string passportPK);
    }
}
