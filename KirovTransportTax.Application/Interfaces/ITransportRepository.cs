using KirovTransportTax.Core.Entities;

namespace KirovTransportTax.Application.Interfaces
{
    public interface ITransportRepository : IRepository<Transport>
    {
        Task Create(Transport entity);
        void Delete(Transport entity);
        void Update(Transport entity);
        void Update(string oldNumberTransporPK, Transport entity);

        Task<IEnumerable<Transport>> GetByNumber(string numberTransporPK);
        Task<IEnumerable<Transport>> GetByPassport(string passportPK);
    }
}
