using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dto
{
    public  record UserLoginAuth
    {
        [Required(ErrorMessage = "Username is reguired")]
        public string? UserName { get; init; }
        [Required(ErrorMessage = "Password is reguired")]
        public string? Password { get; init; }

    }
}
