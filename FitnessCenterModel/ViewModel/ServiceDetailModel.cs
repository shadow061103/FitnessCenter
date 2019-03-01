using FitnessCenterModel.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace FitnessCenterModel.ViewModel
{
   public class ServiceDetailModel
    {
        public CoachService Service { get; set; }
        public SelectList StatusList { get; set; }
    }
    public class ServiceDetail
    {
        public CoachService Service { get; set; }
        public List<ReserveStatus> Status { get; set; }
    }

}
