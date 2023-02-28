using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class Race
    {
        [Column("RaceId")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Count horses is a required field")]
        [Range(5, 15)]
        public int CountHorses { get; set; }

        [Required(ErrorMessage = "Date is a required field")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Competition Name is a required field")]
        [MaxLength(30, ErrorMessage = "Maximum length for the Competition Name is 30 characters.")]
        public string? CompetitionName { get; set; }

        public ICollection<Entry>? Entries { get; set; }
    }
}
