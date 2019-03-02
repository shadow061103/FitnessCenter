using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessCenterModel.Para
{
   public class UpdateInterview
    {
        public int OrderId { get; set; }
        public int StatusId { get; set; }
        public string Location { get; set; }
    }
    public class UpdateServicePara: UpdateInterview
    {

    }
}
