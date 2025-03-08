using Entities.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.Contracts.IRepos
{
    public interface IReminderRepository : IRepositoryBase<Reminder>
    {
        IEnumerable<Reminder> GetAllReminders();
        Reminder GetReminderById(int id);
        void CreateReminder(Reminder reminder);
        void UpdateReminder(Reminder reminder);
        void DeleteReminder(Reminder reminder);
        Task<List<Reminder>> GetRemindersByUserIdAsync(string userId);
    }
}
