using KirovTransportTax.Application.Brands.Commands;
using KirovTransportTax.Application.Brands.Queries;
using KirovTransportTax.Application.Interfaces.Repositories;
using KirovTransportTax.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace KirovTransportTax.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BrandsController : Controller
    {
        private readonly CreateBrandCommand createBrandCommand;
        private readonly DeleteBrandCommand deleteBrandCommand;
        private readonly UpdateBrandCommand updateBrandCommand;
        private readonly GetAllBrandsQuery getAllBrandsQuery;

        public BrandsController(IBrandRepository brandRepository)
        {
            createBrandCommand = new(brandRepository);
            deleteBrandCommand = new(brandRepository);
            updateBrandCommand = new(brandRepository);
            getAllBrandsQuery = new(brandRepository);
        }

        [HttpPost]
        public IActionResult Create(Brand brand)
        {
            if (createBrandCommand.Execute(brand))
                return Ok(brand);
            return BadRequest();
        }

        [HttpPut]
        public IActionResult Update(BrandDto brand)
        {
            try
            {
                if (updateBrandCommand.Execute(brand))
                    return Ok();
                return NotFound();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        public IActionResult Delete(Brand brand)
        {
            try
            {
                if (deleteBrandCommand.Execute(brand))
                    return Ok();
                return NotFound();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet]
        public IEnumerable<Brand> GetAll()
        {
            return getAllBrandsQuery.Execute();
        }
    }
}
