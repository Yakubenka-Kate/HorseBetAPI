using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HorseBet.Models
{
    public class Entry
    {
        [Column("EntryId")]
        public Guid Id { get; set; }

        public int Result { get; set; }

        public double Coefficient { get; set; }

        [ForeignKey(nameof(Race))]
        public Guid RaceId { get; set; }
        public Race? Race { get; set; }

        [ForeignKey(nameof(Horse))]
        public Guid HorseId { get; set; }
        public Horse? Horse { get; set; }

    }
}
