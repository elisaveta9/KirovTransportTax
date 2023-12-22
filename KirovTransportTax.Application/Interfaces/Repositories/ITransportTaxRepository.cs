using KirovTransportTax.Domain.Entities;

namespace KirovTransportTax.Application.Interfaces.Repositories
{
    public interface ITransportTaxRepository : IRepository<TransportTax>
    {
        Task<TransportTax?> GetByNumber(string numberTransportPK);
    }
}
