using KirovTransportTax.Application.Interfaces.Repositories;
using KirovTransportTax.Application.TransportModels.Commands;
using KirovTransportTax.Application.TransportModels.Queries;
using KirovTransportTax.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace KirovTransportTax.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class TransportModelsController : Controller
    {
        private readonly CreateTransportModelCommand createTransportModelCommand;
        private readonly DeleteTransportModelCommand deleteTransportModelCommand;
        private readonly DeleteTransportModelByModelCommand deleteTransportModelByModelCommand;
        private readonly UpdateTransportModelCommand updateTransportModelCommand;

        private readonly GetAllTransportModelsQuery getAllTransportModelsQuery;
        private readonly GetTransportModelDetailsQuery getTransportModelDetailsQuery;

        public TransportModelsController(ITransportModelRepository transportModelRepository)
        {
            createTransportModelCommand = new(transportModelRepository);
            deleteTransportModelCommand = new(transportModelRepository);
            deleteTransportModelByModelCommand = new(transportModelRepository);
            updateTransportModelCommand = new(transportModelRepository);

            getAllTransportModelsQuery = new(transportModelRepository);
            getTransportModelDetailsQuery = new(transportModelRepository);
        }

        [HttpPost]
        public IActionResult CreateTransportModel(TransportModel transportModel)
        {
            if (createTransportModelCommand.Execute(transportModel))
                return Ok(transportModel);
            return BadRequest();            
        }

        [HttpGet("{model}")]
        public IActionResult GetTransportModel(string model)
        {
            try
            {
                var transportModel = getTransportModelDetailsQuery.Execute(model);
                if (transportModel == null)
                    return NoContent();
                return Ok(transportModel);
            } catch
            {
                return BadRequest();
            }
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var models = getAllTransportModelsQuery.Execute();
                return Ok(models);
            } catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public IActionResult Update(string? oldModelName, TransportModel transportModel)
        {
            try
            {
                var result = oldModelName == null ? updateTransportModelCommand.Execute(transportModel) :
                    updateTransportModelCommand.Execute(oldModelName, transportModel);
                if (result)
                    return Ok(result);
                return BadRequest();
            } catch
            {
                return BadRequest();
            }
        }

        [HttpDelete("{model}")]
        public IActionResult Delete(string model)
        {
            if (deleteTransportModelByModelCommand.Execute(model))
                return Ok();
            return NotFound();
        }

        [HttpDelete]
        public IActionResult Delete(TransportModel transportModel)
        {
            if (deleteTransportModelCommand.Execute(transportModel))
                return Ok();
            return NotFound();
        }
    }
}
