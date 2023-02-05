using Entities.Exceptions;
using HorseBet.Models;
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

        [HttpPost]
        public IActionResult CreateHorse([FromBody] HorseForCreation horse)
        {
            if (horse == null)
                return BadRequest("Horse is null");

            var createdHorse = _service.HorseService.CreateHorse(horse);

            return CreatedAtRoute(new { id = createdHorse.Id },createdHorse);
        }
    }
}
