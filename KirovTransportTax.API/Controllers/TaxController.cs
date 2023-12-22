using KirovTransportTax.Application.Interfaces.Repositories;
using KirovTransportTax.Application.Taxs.Queries;
using Microsoft.AspNetCore.Mvc;

namespace KirovTransportTax.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class TaxController : Controller
    {
        private readonly GetAllDriverTaxQuery getAllDriverTaxQuery;
        private readonly GetAllTransportTaxQuery getAllTransportTaxQuery;
        private readonly GetDriverTaxByPassportQuery getDriverTaxByPassportQuery;
        private readonly GetTransportTaxByNumberQuery getTransportTaxByNumberQuery;

        public TaxController(IDriverTaxRepository driverTaxRepository,
            ITransportTaxRepository transportTaxRepository)
        {
            getAllDriverTaxQuery = new(driverTaxRepository);
            getAllTransportTaxQuery = new(transportTaxRepository);
            getDriverTaxByPassportQuery = new(driverTaxRepository);
            getTransportTaxByNumberQuery = new(transportTaxRepository);
        }

        [HttpGet("drivers")]
        public IActionResult GetAllDriversTax()
        {
            return Ok(getAllDriverTaxQuery.Execute());
        }

        [HttpGet("drivers/{passport}")]
        public IActionResult GetDriverTax(string passport)
        {
            return Ok(getDriverTaxByPassportQuery.Execute(passport));
        }

        [HttpGet("transports")]
        public IActionResult GetAllTransportsTax()
        {
            return Ok(getAllTransportTaxQuery.Execute());
        }

        [HttpGet("transports/{number}")]
        public IActionResult GetTransportTax(string number)
        {
            return Ok(getTransportTaxByNumberQuery.Execute(number));
        }
    }
}
