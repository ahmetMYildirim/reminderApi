using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dto
{
    public record tokenDto
    {
        public String AccessToken { get; set; }
        public String RefreshToken { get; set; }

    }
}
