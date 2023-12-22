using KirovTransportTax.Application.Interfaces.Repositories;
using KirovTransportTax.Domain.Entities;

namespace KirovTransportTax.Application.Brands.Commands
{
    public class CreateBrandCommand
    {
        private readonly IBrandRepository _brandRepository;

        public CreateBrandCommand(IBrandRepository repository) 
        {
            _brandRepository = repository;
        }

        public bool Execute(Brand brand)
        {
            try
            {
                return _brandRepository.Create(brand).Result != 0;
            } catch
            {
                return false;
            }
        }
    }
}
