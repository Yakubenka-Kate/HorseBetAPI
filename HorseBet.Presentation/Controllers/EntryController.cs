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
    [Route("api/entries")]
    [ApiController]
    public class EntryController : ControllerBase
    {
        private readonly IServiceManager _service;

        public EntryController(IServiceManager service) => _service = service;

        [HttpGet]
        public IActionResult GetEntries()
        {
            var entries = _service.EntryService.GetAllEntriesAsync(trackChanges: false);

            return Ok(entries);
        }

        [HttpGet("{id:guid}")]
        public IActionResult GetEntry(Guid horseId, Guid id)
        {
            var entry = _service.EntryService.GetEntryForHorseAsync(horseId, id,  trackChanges: false);

            return Ok(entry);
        }

        [HttpGet("{horseId}/horses")]
        public IActionResult GetEntriesForHorse(Guid horseId)
        {
            var entries = _service.EntryService.GetEntriesForHorseAsync(horseId, trackChanges: false);

            return Ok(entries);
        }

        [HttpPost]
        public IActionResult CreateEntry(Guid raceId, Guid horseId, [FromBody] EntryManipulationDto entry)
        {
            if (entry is null)
                return BadRequest("Entry is null");

            var createdEntry = _service.EntryService.CreateEntryAsync(raceId, horseId, entry, trackChanges: false);

            return CreatedAtRoute(new { raceId, horseId, id = createdEntry.Id }, createdEntry);
        }

        [HttpDelete("{id:guid}")]
        public IActionResult DeleteEntryForHorse(Guid horseId, Guid id)
        {
            _service.EntryService.DeleteEntryForHorseAsync(horseId, id, trackChanges: false);

            return NoContent();
        }

        [HttpPut("{id:guid}")]
        public IActionResult UpdateEntryForHorse(Guid horseId, Guid id, [FromBody] EntryManipulationDto entry)
        {
            if (entry is null)
                return BadRequest("Entry is null");

            _service.EntryService.UpdateEntryForHorseAsync(horseId, id, entry, horseTrackChanges: false, entryTrackChanges: true);

            return NoContent();
        }

    }
}
