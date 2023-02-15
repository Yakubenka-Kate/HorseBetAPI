using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public record BetDto(Guid Id, int BetPosition, double Rate);
    public record BetManipulationDto
    {
        [Required(ErrorMessage = "Bet position is a required field")]
        public int BetPosition { get; init; }

        [Required(ErrorMessage = "Bet position is a required field")]
        public double Rate { get; init; }
        public string? UserId { get; init; }
    }
}
