using KirovTransportTax.Domain.Entities;

namespace KirovTransportTax.Application.Interfaces.Repositories
{
    public interface IDriverRepository : IRepository<Driver>
    {
        Task<int> Create(Driver entity);
        void Delete(Driver entity);
        void DeleteByPassport(string passportPK);
        void Update(Driver entity);
        void Update(string oldPassportPK, Driver entity);

        Task<Driver> GetByPassport(string passportPK);
    }
}
