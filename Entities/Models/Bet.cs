using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HorseBet.Models
{
    public class Bet
    {
        [Column("BetId")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Bet position is a required field")]
        public int BetPosition { get; set; }

        [Required(ErrorMessage = "Bet position is a required field")]
        public double Rate { get; set; }

        public string? Result { get; set; }

        [ForeignKey(nameof(Entry))]
        public Guid EntryId { get; set; }
        public Entry? Entry { get; set; }

        [ForeignKey(nameof(User))]
        public string? UserId { get; set; }
        public User? User { get; set; }
    }
}
