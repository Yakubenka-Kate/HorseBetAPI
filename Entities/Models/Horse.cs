using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HorseBet.Models
{
    public class Horse
    {
        [Column("HorseId")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Horse name is a required field")]
        [MaxLength(30, ErrorMessage = "Maximum length for the Horse Name is 30 characters.")]
        public string? HorseName { get; set; }

        [Required(ErrorMessage = "Genger is a required field")]
        public string? Gender { get; set; }

        public string? Owner { get; set; }

        public ICollection<Entry>? Entries { get; set; }
    }
}
