using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects.ManipulationDto
{
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
}
