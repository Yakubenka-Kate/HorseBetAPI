using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public record HorseDto(Guid Id, string HorseName, string Gender, string Owner);
    public record HorseForCreation(string HorseName, string Gender, string Owner);

}
