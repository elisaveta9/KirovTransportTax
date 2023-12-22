using KirovTransportTax.Application.Drivers.Commands;
using KirovTransportTax.Application.Drivers.Queries;
using KirovTransportTax.Application.Interfaces.Repositories;
using KirovTransportTax.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace KirovTransportTax.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class DriversController : Controller
    {
        private readonly CreateDriverCommand createDriverCommand;
        private readonly UpdateDriverCommand updateDriverCommand;
        private readonly DeleteDriverByPassportCommand deleteDriverByPassportCommand;
        private readonly DeleteDriverCommand deleteDriverCommand;

        private readonly GetAllDriversQuery getAllDriversQuery;
        private readonly GetDriverDetailsQuery getDriverDetailsQuery;

        public DriversController(IDriverRepository driverRepository)
        {
            createDriverCommand = new(driverRepository);
            updateDriverCommand = new(driverRepository);
            deleteDriverByPassportCommand = new(driverRepository);
            deleteDriverCommand = new(driverRepository);

            getAllDriversQuery = new(driverRepository);
            getDriverDetailsQuery = new(driverRepository);
        }

        [HttpPost]
        public IActionResult CreateAsync([FromBody] Driver driver)
        {
            try
            {
                if (createDriverCommand.Execute(driver))
                    return Ok(driver);
                return Ok();
            } catch
            {
                return BadRequest();
            }
        }

        [HttpGet("{passport}")]
        public IActionResult GetDriverInfo(string passport) 
        {
            try
            {
                var driver = getDriverDetailsQuery.Execute(passport);
                if (driver == null)
                    return NotFound();
                return Ok(driver);
            } catch
            {
                return BadRequest();
            }
        }

        [HttpGet]
        public IActionResult GetDrivers()
        {
            try
            {
                var drivers = getAllDriversQuery.Execute().ToList();                
                return Ok(drivers ?? new List<Driver>());
            } catch
            {
                return BadRequest();
            }
        }

        [HttpPut]
        public IActionResult Update(string? oldPassport, [FromBody] Driver driver)
        {
            try
            {
                var result = oldPassport == null ? updateDriverCommand.Execute(driver) :
                    updateDriverCommand.Execute(oldPassport, driver);
                if (result)
                    return Ok();
                return NotFound();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpDelete("{passport}")]
        public IActionResult Delete(string passport)
        {
            try
            {
                if (deleteDriverByPassportCommand.Execute(passport))
                    return Ok();
                return NotFound();
            } catch
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        public IActionResult Delete([FromBody] Driver driver)
        {
            try
            {
                if (deleteDriverCommand.Execute(driver))
                    return Ok();
                return NotFound();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
