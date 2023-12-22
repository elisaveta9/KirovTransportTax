using KirovTransportTax.Application.Brands.Commands;
using KirovTransportTax.Application.Drivers.Commands;
using KirovTransportTax.Application.Interfaces.Repositories;
using KirovTransportTax.Application.TransportModels.Commands;
using KirovTransportTax.Application.Transports.Commands;
using KirovTransportTax.Application.Transports.Queries;
using KirovTransportTax.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace KirovTransportTax.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class TransportsController : Controller
    {
        private readonly CreateTransportCommand createTransportCommand;
        private readonly DeleteTransportCommand deleteTransportCommand;
        private readonly DeleteTransportByNumberCommand deleteTransportByNumberCommand;
        private readonly DeleteTransportByPassportCommand deleteTransportByPassportCommand;
        private readonly UpdateTransportCommand updateTransportCommand;

        private readonly GetAllTransportsQuery getAllTransportsQuery;
        private readonly GetTransportDetailsQuery getTransportDetailsQuery;

        public TransportsController(ITransportRepository transportRepository,
            IBrandRepository brandRepository,
            IDriverRepository driverRepository,
            ITransportModelRepository transportModelRepository)
        {
            CreateBrandCommand createBrandCommand = new(brandRepository);
            CreateDriverCommand createDriverCommand = new(driverRepository);
            CreateTransportModelCommand createTransportModelCommand = new(transportModelRepository);

            createTransportCommand = new(transportRepository,
                createBrandCommand,
                createTransportModelCommand,
                createDriverCommand);
            deleteTransportCommand = new(transportRepository);
            deleteTransportByNumberCommand = new(transportRepository);
            deleteTransportByPassportCommand = new(transportRepository);
            updateTransportCommand = new(transportRepository);

            getAllTransportsQuery = new(transportRepository);
            getTransportDetailsQuery = new(transportRepository);
        }

        [HttpPost]
        public IActionResult Create(Transport transport)
        {
            if (createTransportCommand.Execute(transport))
                return Ok();
            return BadRequest();
        }

        [HttpPost("details/")]
        public IActionResult Create(CreateTransportDto transport)
        {
            try
            {
                if (createTransportCommand.Execute(transport))
                    return Ok();
                return BadRequest();
            } catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(getAllTransportsQuery.Execute());
        }

        [HttpGet("{number}")]
        public IActionResult GetTransport(string number)
        {
            try
            {
                var transport = getTransportDetailsQuery.Execute(number);
                if (transport == null)
                    return NotFound();
                return Ok(transport);
            } catch
            {
                return BadRequest();
            }
        }

        [HttpPut]
        public IActionResult Update(Transport transport)
        {
            try
            {
                return Ok(updateTransportCommand.Execute(transport));
            } catch
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        public IActionResult Delete(Transport transport)
        {
            if (deleteTransportCommand.Execute(transport))
                return Ok();
            return BadRequest();
        }

        [HttpDelete("bydetails/")]
        public IActionResult Delete(string? number, string? passport)
        {
            IActionResult result = BadRequest();
            if (number != null && deleteTransportByNumberCommand.Execute(number))
                result = Ok();
            if (passport != null && deleteTransportByPassportCommand.Execute(passport))
                result = Ok();
            return result;
        }
    }
}
