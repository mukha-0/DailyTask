using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DailyTask.Domain.Enums;
using DailyTask.Service.Services.DailyTask.Models;

namespace DailyTask.Service.Services.DailyTask
{
    public interface IDailyTask
    {
        Task<List<DailyTaskGetModel>> GetAllAsync();
        Task<DailyTaskGetModel?> GetByIdAsync(int id);
        Task CreateAsync(DailyTaskCreateModel task);
        Task<bool> UpdateAsync(int id, DailyTaskUpdateModel task);
        Task DeleteAsync(int id);
        Task ChangeStatusAsync(int id, TaskStatuss status);
    }
}
