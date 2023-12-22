using AutoMapper;
using KirovTransportTax.Application.Interfaces.Repositories;
using KirovTransportTax.Domain.Entities;

namespace KirovTransportTax.Application.Brands.Commands
{
    public class UpdateBrandCommand
    {
        private readonly IBrandRepository _brandRepository;
        private readonly Mapper mapper = new(new MapperConfiguration(cnf =>
        {
            cnf.CreateMap<BrandDto, Brand>();
        }));

        public UpdateBrandCommand(IBrandRepository brandRepository)
        {
            _brandRepository = brandRepository;
        }

        public bool Execute(BrandDto brand)
        {
            return _brandRepository.Update(brand.oldName, mapper.Map<Brand>(brand)).Result != 0;
        }
    }

    public class BrandDto
    {
        public string oldName { get; set; }
        public string Name { get; set; } 
    }
}
