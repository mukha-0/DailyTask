using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DailyTask.Domain.Enums;

namespace DailyTask.Domain.Entities
{
    public class DailyTask : Auditable
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public TaskStatuss Status { get; set; }
        private DateTime _startTime;
        public DateTime StartTime
        {
            get => _startTime;
            set => _startTime = DateTime.SpecifyKind(value, DateTimeKind.Utc);
        }

        private DateTime _endTime;
        public DateTime EndTime
        {
            get => _endTime;
            set => _endTime = DateTime.SpecifyKind(value, DateTimeKind.Utc);
        }
    }
}
