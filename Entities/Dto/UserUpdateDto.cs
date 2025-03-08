using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dto
{
    public class UserUpdateDto
    {
        public string username { get; set; }
        public string email { get; set; }
        public string phone_number {  get; set; }
        public string password { get; set; }
    }
}
