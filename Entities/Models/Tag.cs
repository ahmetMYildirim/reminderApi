using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Tag
    {
        public int id { get; set; }
        public string tagName { get; set; }

        public ICollection<Reminder_Tag> ReminderTags { get; set; }
    }
}
