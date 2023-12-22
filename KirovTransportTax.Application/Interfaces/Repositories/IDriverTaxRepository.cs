using KirovTransportTax.Domain.Entities;

namespace KirovTransportTax.Application.Interfaces.Repositories
{
    public interface IDriverTaxRepository : IRepository<DriverTax>
    {
        Task<IEnumerable<DriverTax>> GetByPassport(string passport);
    }
}
