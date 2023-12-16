using KirovTransportTax.Core.Entities;

namespace KirovTransportTax.Application.Interfaces
{
    public interface IDriverTaxRepository : IRepository<DriverTax>
    {
        Task<IEnumerable<DriverTax>> GetByPassport(string passport);
    }
}
