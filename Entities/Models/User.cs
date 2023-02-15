using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorseBet.Models
{
    public class User : IdentityUser
    {
        public double Balance { get; set; }
    }
}
