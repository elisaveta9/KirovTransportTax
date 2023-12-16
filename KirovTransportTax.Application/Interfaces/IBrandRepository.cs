using KirovTransportTax.Core.Entities;

namespace KirovTransportTax.Application.Interfaces
{
    public interface IBrandRepository : IRepository<Brand>
    {
        Task Create(Brand entity);
        void Delete(Brand entity);
        void Update(string oldBrandPK, Brand entity);
    }
}
