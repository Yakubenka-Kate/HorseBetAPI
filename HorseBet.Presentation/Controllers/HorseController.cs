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
        public async Task<IActionResult> GetHorses()
        {
            var horses = await _service.HorseService.GetAllHorsesAsync(trackChanges: false);

            return Ok(horses);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetHorse(Guid id)
        {
            var horse = await _service.HorseService.GetHorseAsync(id, trackChanges: false);

            return Ok(horse);
        }

        [HttpGet("collection")]
        public async Task<IActionResult> GetHorseCollection([ModelBinder(BinderType = typeof(ArrayModelBinder))]IEnumerable<Guid> ids)
        {
            var horses = await _service.HorseService.GetByIdsAsync(ids, trackChanges: false);

            return Ok(horses);
        }


        [HttpPost]
        public async Task<IActionResult> CreateHorse([FromBody] HorseManipulationDto horse)
        {
            if (horse is null)
                return BadRequest("Horse is null");

            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            var createdHorse = await _service.HorseService.CreateHorseAsync(horse);

            return CreatedAtRoute(new { id = createdHorse.Id },createdHorse);
        }

        [HttpPost("collection")]
        public async Task<IActionResult> CreateHorseCollection([FromBody] IEnumerable<HorseManipulationDto> horseCollection)
        {
            var result = await _service.HorseService.CreateHorsesCollectionAsync(horseCollection);

            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            return CreatedAtRoute(new { result.ids }, result.horses);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteHorse(Guid id)
        {
            await _service.HorseService.DeleteHorseAsync(id, trackChanges: false);

            return NoContent();
        }

    }

}
