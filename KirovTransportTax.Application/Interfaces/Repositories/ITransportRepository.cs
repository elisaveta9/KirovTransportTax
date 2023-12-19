using KirovTransportTax.Domain.Entities;

namespace KirovTransportTax.Application.Interfaces.Repositories
{
    public interface ITransportRepository : IRepository<Transport>
    {
        Task<int> Create(Transport entity);
        void Delete(Transport entity);
        void DeleteByNumber(string numberTransporPK);
        void DeleteByPssport(string passportFK);
        void Update(Transport entity);
        void Update(string oldNumberTransporPK, Transport entity);

        Task<Transport> GetByNumber(string numberTransporPK);
        Task<IEnumerable<Transport>> GetByPassport(string passportPK);
    }
}
