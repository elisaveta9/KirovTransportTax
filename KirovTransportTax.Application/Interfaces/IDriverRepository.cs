using KirovTransportTax.Domain.Entities;

namespace KirovTransportTax.Application.Interfaces
{
    public interface IDriverRepository : IRepository<Driver>
    {
        Task Create(Driver entity);
        void Delete(Driver entity);
        void Update(Driver entity);
        void Update(string oldPassportPK, Driver entity);

        Task<IEnumerable<Driver>> GetByPassport(string passportPK);
    }
}
