using KirovTransportTax.Core.Entities;

namespace KirovTransportTax.Application.Interfaces.Repositories
{
    public interface ITransportTaxRepository : IRepository<TransportTax>
    {
        Task<IEnumerable<TransportTax>> GetByNumber(string numberTransportPK);
    }
}
