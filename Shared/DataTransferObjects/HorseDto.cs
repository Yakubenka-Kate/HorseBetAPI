using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public record HorseDto(Guid Id, string HorseName, string Gender, string Owner);

    public record HorseManipulationDto
    {
        [Required(ErrorMessage = "Horse name is a required field")]
        [MaxLength(30, ErrorMessage = "Maximum length for the Horse Name is 30 characters.")]
        public string? HorseName { get; init; }

        [Required(ErrorMessage = "Genger is a required field")]
        [RegularExpression(@"male|female", ErrorMessage = "Gender must be male or female.")]
        public string? Gender { get; init; }

        [Required(ErrorMessage = "Owner name is a required field")]
        [MaxLength(30, ErrorMessage = "Maximum length for the Owner Name is 30 characters.")]
        public string? Owner { get; init; }
    }

    public record RaceDto(Guid Id, int CountHorses, DateTime Date, string CompetitionName);
    public record RaceManipulationDto
    {
        [Required(ErrorMessage = "Count horses is a required field")]
        [Range(5, 15)]
        public int CountHorses { get; init; }

        [Required(ErrorMessage = "Date is a required field")]
        public DateTime Date { get; init; }

        [Required(ErrorMessage = "Competition Name is a required field")]
        [MaxLength(30, ErrorMessage = "Maximum length for the Competition Name is 30 characters.")]
        public string? CompetitionName { get; init; }
    }

    public record EntryDto(Guid Id, int Result, double Coefficient);
    public record EntryManipulationDto(int Result, double Coefficient);

    public record BetDto(Guid Id, int BetPosition, double Rate);
    public record BetManipulationDto
    {
        [Required(ErrorMessage = "Bet position is a required field")]
        public int BetPosition { get; init; }

        [Required(ErrorMessage = "Bet position is a required field")]
        public double Rate { get; init; }
    }

}
