using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public sealed class RaceNotFoundException : NotFoundException
    {
        public RaceNotFoundException(Guid raceId) : base($"The race with id: {raceId} doesn't exist in the database!")
        { }
    
    }
}
