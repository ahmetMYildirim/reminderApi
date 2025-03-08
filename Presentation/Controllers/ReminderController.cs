using Entities.Dto;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "v1")]
    public class ReminderController : ControllerBase
    {
        private readonly IServiceManager _manager;

        public ReminderController(IServiceManager manager)
        {
            _manager = manager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllReminders([FromQuery] ReminderFilterDto filterDto)
        {
            try
            {
                var pagedList = await _manager.reminderServices.GetAll(filterDto);
                return Ok(pagedList.reminders);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}", Name = "GetReminderById")]
        public async Task<IActionResult> GetReminderById(int id)
        {
            var reminder =  _manager.reminderServices.GetReminderById(id);  

            if (reminder == null)
                return NotFound();

            return Ok(reminder);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOneReminder([FromBody] ReminderCreateDto reminderDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var reminder = new Reminder
            {
                user_id = reminderDto.user_id,
                title = reminderDto.title,
                description = reminderDto.description,
                dueTime = reminderDto.dueTime,
                status = reminderDto.status,
                created_at = DateTime.UtcNow,
                updated_at = DateTime.UtcNow
            };

             _manager.reminderServices.CreateReminder(reminder);

            return CreatedAtRoute("GetReminderById", new { id = reminder.id }, reminder);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReminder(int id, [FromBody] ReminderForUpdateDto reminderUpdate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingReminder =  _manager.reminderServices.GetReminderById(id);  
            if (existingReminder == null)
            {
                return NotFound();
            }

            existingReminder.title = reminderUpdate.title;
            existingReminder.description = reminderUpdate.description;
            existingReminder.dueTime = reminderUpdate.dueTime;
            existingReminder.status = reminderUpdate.status;
            existingReminder.updated_at = DateTime.UtcNow;

            await _manager.reminderServices.UpdateReminder(id, existingReminder);  

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReminder([FromRoute(Name = "id")] int id)  
        {
            try
            {
                 _manager.reminderServices.DeleteReminder(id);  
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetRemindersByUserId(string userId)
        {
            var reminders = await _manager.reminderServices.GetRemindersByUserIdAsync(userId);

            if (reminders == null || reminders.Count == 0)
            {
                return NotFound("No reminders found for the specified user.");
            }

            return Ok(reminders);
        }

    }
}
