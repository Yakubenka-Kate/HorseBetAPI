using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HorseBet.Models
{
    public class Bet
    {
        [Column("BetId")]
        public Guid Id { get; set; }

        [Required]
        public int BetPosition { get; set; }

        [Required]
        public double Rate { get; set; }

        public string? Result { get; set; }

        [ForeignKey(nameof(EntryId))]
        public Guid EntryId { get; set; }
        public Entry? Entry { get; set; }

        //public string UserId { get; set; }
    }
}
