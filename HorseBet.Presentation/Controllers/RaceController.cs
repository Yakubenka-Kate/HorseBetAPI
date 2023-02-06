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
        public IActionResult GetRaces()
        {
            var races = _service.RaceService.GetAllRaces(trackChanges: false);

            return Ok(races);
        }

        [HttpGet("{id:guid}")]
        public IActionResult GetRace(Guid id)
        {
            var race = _service.RaceService.GetRace(id, trackChanges: false);

            return Ok(race);
        }

        [HttpPost]
        public IActionResult CreateHorse([FromBody] RaceForCreationDto race)
        {
            if (race is null)
                return BadRequest("Horse is null");

            var createdRace = _service.RaceService.CreateRace(race);

            return CreatedAtRoute(new { id = createdRace.Id }, createdRace);
        }
    }
}
