using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dto
{
    public class Reminder_TagDto
    {
        public int reminder_id { get; set; }
        public int tag_id { get; set; }
        
        public TagDto tag { get; set; }
    }
}
