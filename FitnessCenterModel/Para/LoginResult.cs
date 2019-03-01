using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessCenterModel
{
    public class LoginResult<T> : Result 
    {
        public T Memberinfo { get; set; }
    }
}
