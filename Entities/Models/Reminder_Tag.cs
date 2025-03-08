using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Reminder_Tag
    {
        public int reminder_id { get; set; }
        public int tag_id { get; set; }

        public Reminder Reminder { get; set; }
        public Tag Tag { get; set; }
    }
}
