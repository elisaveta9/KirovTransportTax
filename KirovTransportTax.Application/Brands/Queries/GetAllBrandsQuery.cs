using KirovTransportTax.Application.Interfaces.Repositories;
using KirovTransportTax.Domain.Entities;

namespace KirovTransportTax.Application.Brands.Queries
{
    public class GetAllBrandsQuery
    {
        private readonly IBrandRepository _brandRepository;

        public GetAllBrandsQuery(IBrandRepository brandRepository)
        {
            _brandRepository = brandRepository;
        }

        public IEnumerable<Brand> Execute()
        {
            return _brandRepository.GetAll().Result;
        }
    }
}
