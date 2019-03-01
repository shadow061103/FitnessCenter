using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessCenterModel.Para
{
  public  class ServicePara
    {
        public string CoachId { get; set; }
        public string MemberId { get; set; }
        public int StatusId { get; set; }
        public DateTime ServiceDate { get; set; }
        public int TrainingProgramId { get; set; }
        public int CourseId { get; set; }
        public int Charge { get; set; }
        public string Location { get; set; }
    }
}
