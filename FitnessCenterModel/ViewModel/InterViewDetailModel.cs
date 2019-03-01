using FitnessCenterModel.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace FitnessCenterModel.ViewModel
{
    public class InterViewDetailModel
    {
        public Interview interview { get; set; }

        public SelectList StatusList { get; set; }

    }
    public class InterviewDetail
    {
        public Interview interview { get; set; }
        public List<ReserveStatus> Status { get; set; }
        
    }
}
