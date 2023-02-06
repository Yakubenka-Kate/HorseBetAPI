using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public record HorseDto(Guid Id, string HorseName, string Gender, string Owner);
    public record HorseForCreationDto(string HorseName, string Gender, string Owner);

    public record RaceDto(Guid Id, int CountHorses, DateTime Date, string CompetitionName);
    public record RaceForCreationDto(int CountHorses, DateTime Date, string CompetitionName);

    public record EntryDto(Guid Id, int Result, double Coefficient);
    public record EntryForManipulationsDto(int Result, double Coefficient);

    public record BetDto(Guid Id, int BetPosition, double Rate, string Result);
    public record BetForManipulationsDto(int BetPosition, double Rate, string Result);

}
