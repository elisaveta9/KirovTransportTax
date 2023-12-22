using KirovTransportTax.Application.Interfaces.Repositories;
using KirovTransportTax.Domain.Entities;

namespace KirovTransportTax.Application.Brands.Commands
{
    public class DeleteBrandCommand
    {
        private readonly IBrandRepository _brandRepository;

        public DeleteBrandCommand(IBrandRepository repository)
        {
            _brandRepository = repository;
        }

        public bool Execute(Brand brand)
        {
            return _brandRepository.Delete(brand).Result != 0;
        }
    }
}
