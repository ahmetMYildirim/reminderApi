using Entities.Dto;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using Repository.Contracts;
using Repository.Contracts.IRepos;
using Repository.EFCore;
using Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ReminderServices : IReminderServices
    {
        private readonly IRepositoryManager _repositoryManager;

        public ReminderServices(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        public Reminder CreateReminder(Reminder reminder)
        {
            _repositoryManager.reminderRepository.CreateReminder(reminder);
            _repositoryManager.Save();
            return reminder;
        }

        public bool DeleteReminder(int id)
        {
            var reminder = _repositoryManager.reminderRepository.GetReminderById(id);
            if(reminder == null)
                return false;

            _repositoryManager.reminderRepository.DeleteReminder(reminder);
            _repositoryManager.Save();
            return true;
        }

        public async Task<(IEnumerable<ReminderDto> reminders, MetaData metaData)> GetAll(ReminderFilterDto filterDto)
        {
            var reminderQuery = _repositoryManager.reminderRepository.GetAllReminders()
                                .Where(r => string.IsNullOrEmpty(filterDto.Status) || r.status == filterDto.Status)
                                .Where(r => string.IsNullOrEmpty(filterDto.Title) || r.title.Contains(filterDto.Title));
            
            var reminders =reminderQuery
                            .Skip((filterDto.PageNumber - 1) * filterDto.PageSize)
                            .Take(filterDto.PageSize)
                            .ToList();

            var metaData = new MetaData()
            {
                TotalCount = reminderQuery.Count(),
                PageSize = filterDto.PageSize,
                CurrentPage = filterDto.PageNumber,
                TotalPages = (int)Math.Ceiling((double) reminderQuery.Count() / filterDto.PageSize)
            };

            var reminderDtos = reminders.Select(r => new ReminderDto()
            {
                id = r.id,
                title = r.title,
                description = r.description,
                dueTime = r.dueTime,
                status = r.status,
                created_at = r.created_at,
                updated_at = r.updated_at,
            });

            return (reminderDtos, metaData);

        }

        public async Task<IEnumerable<ReminderDto>> GetAll()
        {
            var reminders = _repositoryManager.reminderRepository.GetAllReminders().ToList();
            return reminders.Select(r => new ReminderDto
            {
                id = r.id,
                title = r.title,
                description = r.description,
                dueTime = r.dueTime,
                status = r.status,
                created_at = r.created_at,
                updated_at = r.updated_at,
            });
        }

        public Reminder GetReminderById(int id)
        {
            var reminder = _repositoryManager.reminderRepository.GetReminderById(id);
            if (reminder == null)
                return null;

            return reminder;
        }

        public  Task<List<Reminder>> GetRemindersByUserIdAsync(string userId)
        {
            return _repositoryManager.reminderRepository.GetRemindersByUserIdAsync(userId);
        }

        public async Task UpdateReminder(int id, Reminder reminder)
        {
            var existingReminder = _repositoryManager.reminderRepository.GetReminderById(id); 

            if (existingReminder == null)
                throw new KeyNotFoundException("Reminder not found."); 

            existingReminder.title = reminder.title;
            existingReminder.description = reminder.description;
            existingReminder.status = reminder.status;
            existingReminder.dueTime = reminder.dueTime;
            existingReminder.updated_at = DateTime.UtcNow;

            await _repositoryManager.Save(); 
        }
    }

}



