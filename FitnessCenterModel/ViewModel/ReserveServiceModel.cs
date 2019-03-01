using FitnessCenterModel.DTO;
using FitnessCenterModel.Para;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace FitnessCenterModel.ViewModel
{
   public class ReserveServiceModel
    {
        public ServicePara Para { get; set; }
        public SelectList TrainProgramList { get; set; }
        public SelectList CourseList { get; set; }
        public string CoachName { get; set; }
    }
}
