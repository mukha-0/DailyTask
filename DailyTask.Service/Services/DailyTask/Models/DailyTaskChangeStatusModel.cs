using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DailyTask.Domain.Enums;

namespace DailyTask.Service.Services.DailyTask.Models
{
    public class DailyTaskChangeStatusModel
    {
        public int Id { get; set; }
        public TaskStatuss Status { get; set; }
    }
}
