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
    [Route("api/races")]
    [ApiController]
    public class RaceController : ControllerBase
    {
        private readonly IServiceManager _service;

        public RaceController(IServiceManager service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> GetRaces()
        {
            var races = await _service.RaceService.GetAllRacesAsync(trackChanges: false);

            return Ok(races);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetRace(Guid id)
        {
            var race = await _service.RaceService.GetRaceAsync(id, trackChanges: false);

            return Ok(race);
        }

        [HttpPost]
        public async Task<IActionResult> CreateHorse([FromBody] RaceManipulationDto race)
        {
            if (race is null)
                return BadRequest("Horse is null");

            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            var createdRace = await _service.RaceService.CreateRaceAsync(race);

            return CreatedAtRoute(new { id = createdRace.Id }, createdRace);
        }
    }
}
