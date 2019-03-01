using FitnessCenterModel.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessCenterModel.Para
{
    //預約面談或預約服務
    public class SearchServicePara 
    {
        public string MemberId { get; set; }
        public Status Type { get; set; } //0學員1教練
    }
}
