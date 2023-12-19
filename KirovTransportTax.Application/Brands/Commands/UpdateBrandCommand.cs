using KirovTransportTax.Application.Interfaces.Repositories;
using KirovTransportTax.Domain.Entities;

namespace KirovTransportTax.Application.Brands.Commands
{
    public class UpdateBrandCommand
    {
        private readonly IBrandRepository _brandRepository;

        public UpdateBrandCommand(IBrandRepository brandRepository)
        {
            _brandRepository = brandRepository;
        }

        public void Execute(string oldBrandPK, Brand brand)
        {
            _brandRepository.Update(oldBrandPK, brand);
        }
    }
}
