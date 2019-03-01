using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FitnessCenterAPI.Facade;
using FitnessCenterModel;
using FitnessCenterModel.DTO;

namespace FitnessCenterAPI.Controllers
{
    //通用資料
    public class DataController : ApiController
    {
        ShareFacade facade = new ShareFacade();
        //地區
        [HttpPost]
        public List<TaiwanCountry> GetCountryList()
        {
            return facade.GetCountryList();
        }
        //訓練計畫
        [HttpPost]
        public List<FitnessTrainingProgram> GetTrainingProgramList()
        {
            return facade.GetTrainingProgramList();
        }
        //課程形式
        [HttpPost]
        public List<FitnessCourse> GetCourseList()
        {
            return facade.GetCourseList();
        }
    }
}
