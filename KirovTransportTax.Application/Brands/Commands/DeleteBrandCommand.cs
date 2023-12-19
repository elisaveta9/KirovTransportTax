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

        public void Execute(Brand brand)
        {
            _brandRepository.Delete(brand);
        }
    }
}
