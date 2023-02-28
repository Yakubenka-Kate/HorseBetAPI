using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public record BetsForEntriesDto
    {
        public Guid Id { get; set; }
        public int? Result { get; set; }
        public double? Coefficient { get; set; }
        public Guid? BetId { get; set; }
        public int? BetPosition { get; init; }
        public double? Rate { get; init; }
        public string? UserId { get; init; }
        
    }
}
