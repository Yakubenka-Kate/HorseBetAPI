using Microsoft.AspNetCore.Authorization;
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
    [Route("api/bets")]
    [ApiController]
    public class BetController : ControllerBase
    {
        private readonly IServiceManager _service;

        public BetController(IServiceManager service) => _service = service;

        [HttpGet]
        //[Authorize]
        public async Task<IActionResult> GetBets()
        {
            var bets = await _service.BetService.GetAllBetsAsync(trackChanges: false);

            return Ok(bets);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetBet(Guid entryId, Guid id)
        {
            var bet = await _service.BetService.GetBetForEntryAsync(entryId, id, trackChanges: false);

            return Ok(bet);
        }

        [HttpGet("{entryId}/entries")]
        public async Task<IActionResult> GetBetsForEntry(Guid entryId)
        {
            var bets = await _service.BetService.GetBetsForEntryAsync(entryId, trackChanges: false);

            return Ok(bets);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBet( Guid entryId, [FromBody] BetManipulationDto bet)
        {
            if (bet is null)
                return BadRequest("Bet is null");

            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            var createdBet = await _service.BetService.CreateBetAsync(entryId, bet, trackChanges: false);

            return CreatedAtRoute(new { entryId, id = createdBet.Id }, createdBet);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteBetForEntry(Guid entryId, Guid id)
        {
            await _service.BetService.DeleteBetForEntryAsync(entryId, id, trackChanges: false);

            return NoContent();
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateBetForEntry(Guid entryId, Guid id, [FromBody] BetManipulationDto bet)
        {
            if (bet is null)
                return BadRequest("Entry is null");

            if (!ModelState.IsValid)
                return UnprocessableEntity(ModelState);

            await _service.BetService.UpdateBetForEntryAsync(entryId, id, bet, entryTrackChanges: false, betTrackChanges: true);

            return NoContent();
        }
    }
}
