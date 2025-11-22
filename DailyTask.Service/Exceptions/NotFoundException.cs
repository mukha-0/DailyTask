using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyTask.Service.Exceptions
{
    public class NotFoundException : Exception
    {
        public int StatusCode { get; set; }
        public NotFoundException(string message) : base(message)
        {
            StatusCode = 404;
        }
    }
}
