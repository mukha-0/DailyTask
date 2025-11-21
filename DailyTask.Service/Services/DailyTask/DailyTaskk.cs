using DailyTask.DataAccess.Contexts;
using DailyTask.DataAccess.UnitOfWork;
using DailyTask.Domain;
using DailyTask.Domain.Enums;
using DailyTask.Service.Services.DailyTask;
using DailyTask.Service.Services.DailyTask.Models;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DailyTask.Service
{
    public class DailyTaskk(IUnitOfWork unitOfWork,
            IValidator<DailyTaskCreateModel> taskCreateValidator,
            IValidator<DailyTaskUpdateModel> taskUpdateValidator) : IDailyTask
    {
        public async Task ChangeStatusAsync(int id, TaskStatuss status)
        {
            var task = await unitOfWork.DailyTasks.SelectAllAsQueryable()
                .Where(x => x.IsDeleted == false)
                .FirstOrDefaultAsync(t => t.Id == id);
            if (task == null)
            {
                throw new KeyNotFoundException($"Task with id {id} not found.");
            }
            task.Status = status;
            unitOfWork.DailyTasks.Update(task);
            await unitOfWork.SaveAsync();
        }

        public async Task CreateAsync(DailyTaskCreateModel task)
        {
            var validationResult = await taskCreateValidator.ValidateAsync(task);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }
            var newTask = new Domain.Entities.DailyTask
            {
                Name = task.Name,
                Description = task.Description,
                Status = task.Status,
                StartTime = task.StartTime,    
                EndTime = task.EndTime,          
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                DeletedAt = DateTime.MinValue,   
                IsDeleted = false
            };
            unitOfWork.DailyTasks.Insert(newTask);
            await unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var task = await unitOfWork.DailyTasks.SelectAllAsQueryable()
                .Where(x => x.IsDeleted == false)
                .FirstOrDefaultAsync(t => t.Id == id);
            if (task == null)
            {
                throw new KeyNotFoundException($"Task with id {id} not found.");
            }
            task.IsDeleted = true;
            unitOfWork.DailyTasks.Update(task);
            await unitOfWork.SaveAsync();
        }

        public async Task<List<DailyTaskGetModel>> GetAllAsync()
        {
            var tasks = unitOfWork.DailyTasks.SelectAllAsQueryable()
                .Where(x => x.IsDeleted == false);

            return await tasks.Select(task => new DailyTaskGetModel
            {
                Id = task.Id,
                Name = task.Name,
                Description = task.Description,
                Status = task.Status,
                StartTime = task.StartTime,
                EndTime = task.EndTime
            }).ToListAsync();
        }

        public async Task<DailyTaskGetModel?> GetByIdAsync(int id)
        {
            var task = await unitOfWork.DailyTasks.SelectAllAsQueryable()
                .Where(x => x.IsDeleted == false)
                .FirstOrDefaultAsync(t => t.Id == id);
            if (task == null) return null;
            return new DailyTaskGetModel
            {
                Id = task.Id,
                Name = task.Name,
                Description = task.Description,
                Status = task.Status,
                StartTime = task.StartTime,
                EndTime = task.EndTime
            };
        }

        public async Task<bool> UpdateAsync(int id,DailyTaskUpdateModel model)
        {
            var task = unitOfWork.DailyTasks.SelectAllAsQueryable()
                .Where(x => x.IsDeleted == false)
                .FirstOrDefault(t => t.Id == id);

            if (task == null) return false;
            task.Name = model.Name;
            task.Description = model.Description;
            task.StartTime = model.StartTime;
            task.EndTime = model.EndTime;
            unitOfWork.DailyTasks.Update(task);
            await unitOfWork.SaveAsync();
            return true;
        }

    }
}
