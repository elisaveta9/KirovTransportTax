using KirovTransportTax.Domain.Entities;

namespace KirovTransportTax.Application.Interfaces.Repositories
{
    public interface IBrandRepository : IRepository<Brand>
    {
        Task<int> Create(Brand entity);
        Task<int> Delete(Brand entity);
        Task<int> Update(string oldBrandPK, Brand entity);
    }
}
