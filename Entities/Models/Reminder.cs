using Entities.Auth_Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Reminder
    {
        public int id { get; set; }
        public string user_id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public DateTime dueTime { get; set; }
        public string status { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }

        public User user_dto { get; set; }
        public ICollection<Reminder_Tag> ReminderTags { get; set; }
    }
}