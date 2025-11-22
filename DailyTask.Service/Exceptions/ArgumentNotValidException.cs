using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyTask.Service.Exceptions
{
    public class ArgumentIsNotValidException : Exception
    {
        public int StatusCode { get; set; }
        public ArgumentIsNotValidException(string message) : base(message)
        {
            StatusCode = 400;
        }
    }
}
