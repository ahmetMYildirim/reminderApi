using Entities.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Auth_Models
{
    public class User : IdentityUser
    {
        public String? password { get; set; }
        public String? email;
        public String? RefreshToken { get; set; }
        public DateTime ResfreshTokenExpiryTime { get; set; }
    }
}
