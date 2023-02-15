using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public class DiscrepancyBadRequest : BadRequestException
    {
        public DiscrepancyBadRequest() : base("The declared number of horses does not match the entered data")
        {
        }
    }
}
