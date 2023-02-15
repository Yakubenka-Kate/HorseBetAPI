using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public sealed class HorseNotFoundException : NotFoundException
    {
        public HorseNotFoundException(Guid horseId) : base($"The horse with id: {horseId} doesn't exist in the database!")
        { }
    }
}
