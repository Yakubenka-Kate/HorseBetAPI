using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public record RaceDto(Guid Id, int CountHorses, DateTime Date, string CompetitionName);
    public record RaceManipulationDto
    {
        [Required(ErrorMessage = "Count horses is a required field")]
        [Range(1, 15)]
        public int CountHorses { get; init; }

        [Required(ErrorMessage = "Date is a required field")]
        public DateTime Date { get; init; }

        [Required(ErrorMessage = "Competition Name is a required field")]
        [MaxLength(30, ErrorMessage = "Maximum length for the Competition Name is 30 characters.")]
        public string? CompetitionName { get; init; }
    }
}
