using FitnessCenterModel.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace FitnessCenterModel.ViewModel
{
  public  class UpdateCoachViewModel
    {
        public UpdateCoach UpdateModel { get; set; }
        public string addressCity { get; set; }

        public string addressArea { get; set; }
        public HttpPostedFileBase Image { get; set; }
        public SelectList addressCityList { get; set; }
        public SelectList addresAreaList { get; set; }

        public MultiSelectList TrainingProgramList { get; set; }
        public MultiSelectList CourseList { get; set; }
        public MultiSelectList AreaList { get; set; }
    }
    public class UpdateCoach {
        public Coach User { get; set; }
        public List<string> OriginArea { get; set; }
        public List<int> OriginTrainProgram { get; set; }
        public List<int> OriginCourse{ get; set; }
        public List<string> OriginLicense { get; set; }
        public List<string> OriginCompetition { get; set; }
        public List<string> OriginExperience { get; set; }
    }
}
