using Entities.Exceptions;
using HorseBet.Models;
using HorseBet.Presentation.ModelBinders;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorseBet.Presentation.Controllers
{
    [Route("api/horses")]
    [ApiController]
    public class HorseController : ControllerBase
    {
        private readonly IServiceManager _service;

        public HorseController(IServiceManager service) => _service = service;

        [HttpGet]
        public IActionResult GetHorses()
        {
            var horses = _service.HorseService.GetAllHorses(trackChanges: false);

            return Ok(horses);
        }

        [HttpGet("{id:guid}")]
        public IActionResult GetHorse(Guid id)
        {
            var horse = _service.HorseService.GetHorse(id, trackChanges: false);

            return Ok(horse);
        }

        [HttpGet("collection")]
        public IActionResult GetHorseCollection([ModelBinder(BinderType = typeof(ArrayModelBinder))]IEnumerable<Guid> ids)
        {
            var horses = _service.HorseService.GetByIds(ids, trackChanges: false);

            return Ok(horses);
        }


        [HttpPost]
        public IActionResult CreateHorse([FromBody] HorseForCreationDto horse)
        {
            if (horse is null)
                return BadRequest("Horse is null");

            var createdHorse = _service.HorseService.CreateHorse(horse);

            return CreatedAtRoute(new { id = createdHorse.Id },createdHorse);
        }

        [HttpPost("collection")]
        public IActionResult CreateHorseCollection([FromBody] IEnumerable<HorseForCreationDto> horseCollection)
        {
            var result = _service.HorseService.CreateHorsesCollection(horseCollection);

            return CreatedAtRoute(new { result.ids }, result.horses );
        }

        [HttpDelete("{id:guid}")]
        public IActionResult DeleteHorse(Guid id)
        {
            _service.HorseService.DeleteHorse(id, trackChanges: false);

            return NoContent();
        }

    }

}
