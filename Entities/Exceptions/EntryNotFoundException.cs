using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public class EntryNotFoundException : NotFoundException
    {
        public EntryNotFoundException(Guid entryId) : base($"The entry with id: {entryId} doesn't exist in the database!")
        {
        }
    }
}
