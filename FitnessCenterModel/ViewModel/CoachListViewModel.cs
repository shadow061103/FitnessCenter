using FitnessCenterModel.DTO;
using FitnessCenterModel.Para;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace FitnessCenterModel.ViewModel
{
  public  class CoachListViewModel
    {
        public CoachSearchView searchPara { get; set; } //搜尋頁帶過來的參數
        public IPagedList<CoachData> coachList { get; set; }//教練列表

        public SelectList AreaList { get; set; }
        public SelectList TrainingProgramList { get; set; }

        public SelectList CourseList { get; set; }
        public int PageIndex { get; set; }
        public CoachListViewModel()
        {
            PageIndex = 1;
        }

    }
}
