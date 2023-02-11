using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{

    public record EntryDto(Guid Id, int Result, double Coefficient);
    public record EntryManipulationDto
    {
        public int Result { get; set; } 
        public double Coefficient { get; set; }
    }
}
