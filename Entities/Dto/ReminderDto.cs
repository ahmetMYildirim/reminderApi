using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dto
{
    public class ReminderDto
    {
        public int id { get; set; }
        public int user_id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public DateTime dueTime { get; set; }
        public string status { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set;}

        UsersDto users { get; set; }
        public ICollection<Reminder_TagDto> tags { get; set; }
    }
}
