using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dto
{
    public record UserF_Reg
    {

        [Required(ErrorMessage = "Username is required")]
        public string? username { get; init; }
        
        [Required(ErrorMessage = "Password is required")]
        public string? password { get; init; }
        public string? email { get; init; }
        public ICollection<string> roles { get; init; } = new List<string>();

        public UserF_Reg()
        {
            roles = new List<string> { "User" };
        }

    }
}
