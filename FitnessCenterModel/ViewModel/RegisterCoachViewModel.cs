using FitnessCenterModel.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace FitnessCenterModel.ViewModel
{
  public  class RegisterCoachViewModel
    {
        public Coach User { get; set; }
        public string addressCity { get; set; }

        public string addressArea { get; set; }
        public HttpPostedFileBase Image { get; set; }


    }
}
