using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessCenterModel.DTO
{
   public class User //要存入cookie的資訊
    {
        public string MemberId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public Status State { get; set; }
    }
}
