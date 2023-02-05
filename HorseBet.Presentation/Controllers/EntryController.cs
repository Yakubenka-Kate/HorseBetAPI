using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorseBet.Presentation.Controllers
{
    [Route("api/entries")]
    [ApiController]
    public class EntryController : ControllerBase
    {
        private readonly IServiceManager _service;

        public EntryController(IServiceManager service) => _service = service;

        [HttpGet]
        public IActionResult GetEntries()
        {
            var entries = _service.EntryService.GetAllEntries(trackChanges: false);

            return Ok(entries);
        }

        [HttpGet("{id:guid}")]
        public IActionResult GetEntry(Guid horseId, Guid id)
        {
            var entry = _service.EntryService.GetEntry(horseId, id,  trackChanges: false);

            return Ok(entry);
        }

        [HttpGet("{horseId}/horses")]
        public IActionResult GetEntriesHorHorse(Guid horseId)
        {
            var entries = _service.EntryService.GetEntriesForHorse(horseId, trackChanges: false);

            return Ok(entries);
        }

    }
}
