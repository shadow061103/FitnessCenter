using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FitnessCenterModel.DTO;
namespace FitnessCenterModel.ViewModel
{
  public  class OrderListModel
    {
        public List<Interview> inserview { get; set; }
        public List<CoachService> service { get; set; }
        public Status State { get; set; }
    }
}
