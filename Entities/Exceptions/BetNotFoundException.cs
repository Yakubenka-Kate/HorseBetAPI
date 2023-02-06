using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public sealed class BetNotFoundException : NotFoundException
    {
        public BetNotFoundException(Guid betId) : base($"The bet with id: {betId} doesn't exist in the database!")
        { }
    }
}
