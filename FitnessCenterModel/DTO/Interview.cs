using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessCenterModel.DTO
{
   public class Interview
    {
        public int OrderId { get; set; }
        public string CoachId { get; set; }
        public string MemberId { get; set; }
        public string MemberName { get; set; }

        public int MemberGenger { get; set; }

        public string MemberEmail { get; set; }
        public string MemberPhone { get; set; }
        public int MemberAge { get; set; }
        public string MemberImage { get; set; }
        public string CoachImage { get; set; }
        public string CoachName { get; set; }


        public string CoachEmail { get; set; }
        public string CoachPhone { get; set; }
        public string LineID { get; set; }
        public int StatusId { get; set; }
        public string Status { get; set; }
        public DateTime ReserveDate { get; set; }

        public string Location { get; set; }
        public string Message { get; set; }


    }
}
