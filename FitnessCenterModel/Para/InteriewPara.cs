using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessCenterModel.Para
{
  public  class InteriewPara
    {
        public string CoachId { get; set; }
        public string MemberId { get; set; }
        public int StatusId { get; set; }
        public DateTime ReserveDate { get; set; }
        public string Message { get; set; }
        public string Location { get; set; }
    }
}
