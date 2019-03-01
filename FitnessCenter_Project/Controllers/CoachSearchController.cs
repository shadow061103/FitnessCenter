using FitnessCenter_Project.Service;
using FitnessCenterModel.Para;
using FitnessCenterModel.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using FitnessCenterModel.DTO;
namespace FitnessCenter_Project.Controllers
{
    //搜尋教練列表&教練資料
    public class CoachSearchController : baseController
    {
        Service.SearchService service = new Service.SearchService();
        private const int PageSize = 6;//每頁呈現筆數
       [HttpPost]
        public ActionResult CoachList(CoachListViewModel model)
        {
            //頁面dropdownlist
            model.AreaList =new SelectList(service.GetCountry(),"Value","Text");
            model.TrainingProgramList = new SelectList(service.GetTrainingProgramList(), "Value", "Text");
            model.CourseList = new SelectList(service.GetCourseList(), "Value", "Text");

            //search
            int pageIndex = model.PageIndex < 1 ? 1 : model.PageIndex;
            var list = new List<CoachData>();
            list= service.GetCoachList(model.searchPara);
            model.coachList = list.OrderBy(c => c.Name).ToPagedList(pageIndex, PageSize);
            
            return View(model);
        }
        public async System.Threading.Tasks.Task<ActionResult> CoachDetail(string MemberId = "17bc479ef762413fac09584de2a1c65e")
        {


            Coach coach = new Coach();
            coach = service.GetCoachDetail(MemberId);
            Location location = await service.TransAddressToLocation(coach.Address);
            CoachDetailViewModel model = new CoachDetailViewModel()
            {
                coach = coach,
                location = location
            };

            return View(model);
        }




    }
}