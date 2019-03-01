using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessCenterModel.DTO
{
  public  class CoachData
    {
        public string MemberId { get; set; }
        public string ImageName { get; set; }
        public string Name { get; set; }
        public string NickName { get; set; }
        public string Intoduction { get; set; }
        public string Email { get; set; }
        public List<string> TrainingProgram { get; set; }
        public List<string> Course { get; set; }
        public List<string> Area { get; set; }

    }
    
    //View查出來一整包資料
    public class CoachView
    {
        public string MemberId { get; set; }
        public string ImageName { get; set; }
        public string Name { get; set; }
        public string NickName { get; set; }
        public string Intoduction { get; set; }
        public string Email { get; set; }

        public int TrainingProgramId { get; set; }
        public string TrainingProgram { get; set; }
        public int CourseId { get; set; }
        public string Course { get; set; }
        public string Area { get; set; }
    }
}
