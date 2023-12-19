using KirovTransportTax.Domain.Entities;

namespace KirovTransportTax.Application.Interfaces.Repositories
{
    public interface IBrandRepository : IRepository<Brand>
    {
        Task<int> Create(Brand entity);
        void Delete(Brand entity);
        void Update(string oldBrandPK, Brand entity);
    }
}
