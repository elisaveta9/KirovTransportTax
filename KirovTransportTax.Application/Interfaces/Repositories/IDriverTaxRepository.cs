using KirovTransportTax.Domain.Entities;

namespace KirovTransportTax.Application.Interfaces.Repositories
{
    public interface IDriverTaxRepository : IRepository<DriverTax>
    {
        Task<DriverTax?> GetByPassport(string passport);
    }
}
