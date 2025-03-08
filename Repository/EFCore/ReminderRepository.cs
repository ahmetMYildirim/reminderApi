using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Contracts.IRepos;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.EFCore
{
    public class ReminderRepository : RepoBase<Reminder>, IReminderRepository
    {
        private readonly ReminderContext _context;

        public ReminderRepository(ReminderContext reminderContext) : base(reminderContext)
        {
            _context = reminderContext;
        }

        public void CreateReminder(Reminder reminder)
        {
            Create(reminder);
        }

        public void DeleteReminder(Reminder reminder)
        {
            Delete(reminder);
        }

        public IEnumerable<Reminder> GetAllReminders()
        {
            return GetAll();
        }

        public Reminder GetReminderById(int id)
        {
            return GetById(id);
        }

        public Task<List<Reminder>> GetRemindersByUserIdAsync(string userId)
        {
            return _context.Reminders
                            .Where(r => r.user_id == userId).ToListAsync();
        }

        public void UpdateReminder(Reminder reminder)
        {
            Update(reminder);
        }
    }
}
