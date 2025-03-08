using Entities.Dto;
using Entities.Models;
using Entities.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Contract
{
    public interface IReminderServices
    {
        Reminder CreateReminder(Reminder reminder);
        Task UpdateReminder(int id, Reminder reminder); 
        bool DeleteReminder(int id);
        Reminder GetReminderById(int id);
        Task<List<Reminder>> GetRemindersByUserIdAsync(string userId);
        Task<(IEnumerable<ReminderDto> reminders, MetaData metaData)> GetAll(ReminderFilterDto filterDto);
        Task<IEnumerable<ReminderDto>> GetAll();
    }
}
