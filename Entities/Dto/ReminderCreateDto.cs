using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dto
{
    public class ReminderCreateDto
    {
        public string user_id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public DateTime dueTime { get; set; }
        public string status { get; set; }

    }
}
