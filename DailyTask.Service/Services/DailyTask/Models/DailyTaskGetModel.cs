using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DailyTask.Domain.Enums;

namespace DailyTask.Service.Services.DailyTask.Models
{
    public class DailyTaskGetModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public TaskStatuss Status { get; set; }
    }
}
