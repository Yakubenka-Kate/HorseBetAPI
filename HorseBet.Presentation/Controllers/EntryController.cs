﻿using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> GetEntries()
        {
            var entries = await _service.EntryService.GetAllEntriesAsync(trackChanges: false);

            return Ok(entries);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetEntry(Guid horseId, Guid id)
        {
            var entry = await _service.EntryService.GetEntryForHorseAsync(horseId, id,  trackChanges: false);

            return Ok(entry);
        }

        [HttpGet("{horseId}/horses")]
        public async Task<IActionResult> GetEntriesForHorse(Guid horseId)
        {
            var entries = await _service.EntryService.GetEntriesForHorseAsync(horseId, trackChanges: false);

            return Ok(entries);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEntry(Guid raceId, Guid horseId, [FromBody] EntryManipulationDto entry)
        {
            if (entry is null)
                return BadRequest("Entry is null");

            var createdEntry = await _service.EntryService.CreateEntryAsync(raceId, horseId, entry, trackChanges: false);

            return CreatedAtRoute(new { raceId, horseId, id = createdEntry.Id }, createdEntry);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteEntryForHorse(Guid horseId, Guid id)
        {
            await _service.EntryService.DeleteEntryForHorseAsync(horseId, id, trackChanges: false);

            return NoContent();
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateEntryForHorse(Guid horseId, Guid id, [FromBody] EntryManipulationDto entry)
        {
            if (entry is null)
                return BadRequest("Entry is null");

            await _service.EntryService.UpdateEntryForHorseAsync(horseId, id, entry, horseTrackChanges: false, entryTrackChanges: true);

            return NoContent();
        }

    }
}
