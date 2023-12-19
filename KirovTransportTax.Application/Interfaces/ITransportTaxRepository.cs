using KirovTransportTax.Core.Entities;

namespace KirovTransportTax.Application.Interfaces
{
    public interface ITransportTaxRepository : IRepository<TransportTax>
    {
        Task<IEnumerable<TransportTax>> GetByNumber(string numberTransportPK);
    }
}
