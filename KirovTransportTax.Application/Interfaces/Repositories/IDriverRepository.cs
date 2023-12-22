using KirovTransportTax.Domain.Entities;

namespace KirovTransportTax.Application.Interfaces.Repositories
{
    public interface IDriverRepository : IRepository<Driver>
    {
        Task<int> Create(Driver entity);
        Task<int> Delete(Driver entity);
        Task<int> DeleteByPassport(string passportPK);
        Task<int> Update(Driver entity);
        Task<int> Update(string oldPassportPK, Driver entity);

        Task<Driver> GetByPassport(string passportPK);
    }
}
