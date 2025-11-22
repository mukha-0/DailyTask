using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyTask.Service.Exceptions
{
    public class AlreadyExistException : Exception
    {
        public int StatusCode { get; set; }
        public AlreadyExistException(string message) : base(message)
        {
            StatusCode = 403;
        }
    }

}
