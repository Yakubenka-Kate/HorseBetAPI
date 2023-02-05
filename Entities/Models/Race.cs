using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HorseBet.Models
{
    public class Race
    {
        [Column("RaceId")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Count horses is a required field")]
        [Range(5, 15)]
        public int CountHorses { get; set; }

        public DateTime Date { get; set; }
        public string? CompetitionName { get; set; }

        public ICollection<Entry>? Entries { get; set; }
    }
}
