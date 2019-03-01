using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessCenterModel
{
   public class Result
    {
        public int ReturnNo { get; set; }
        public string Message { get; set; }
    }
    public class RegisterResult : Result
    {
        public string MemberId { get; set; }
    }
}
