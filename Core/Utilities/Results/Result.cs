using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results
{
    public class Result : IResult
    {

        public Result(bool success, string message):this(success)//17. Satırın çalışması için
        {
            Message = message;
        }

        public Result(bool success)
        {
            Success = success;
        }

        //Getter bir readonly olduğu için constructor'da set edilebilir.

        public bool Success { get; }

        public string Message { get; }
    }
}
