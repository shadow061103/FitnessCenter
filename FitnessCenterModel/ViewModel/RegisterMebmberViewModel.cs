using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FitnessCenterModel.DTO;
using System.Web;

namespace FitnessCenterModel.ViewModel
{
  public  class RegisterMebmberViewModel
    {
        public Member User { get; set; }
        public string addressCity { get; set; }

        public string addressArea { get; set; }
        public HttpPostedFileBase Image { get; set; }

        
    }
}
