using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FitnessCenterModel.DTO;
using System.Web.Security;
using Newtonsoft.Json;
using FitnessCenterModel;
using FitnessCenter_Project.Service;
namespace FitnessCenter_Project.Controllers
{
    public class baseController : Controller
    {
        protected bool LoginState { get; set; }
        // GET: base
        public User GetMemberInfo()
        {
            var user = HttpContext.User;
            if (user.Identity.IsAuthenticated)
            {
                
                var identity = (FormsIdentity)user.Identity;
                //取得 FormsAuthenticationTicket
                var ticket = identity.Ticket;
                return JsonConvert.DeserializeObject<User>(ticket.UserData);
            }
            return null;


        }
        //進action前檢查
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //判斷有無登入
            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                User user = GetMemberInfo();
                TempData["UserName"] = user.Name;
                TempData["State"] = user.State;
                LoginState = true;
               
            }
            else
                LoginState = false;
                
            
            TempData["LoginState"] = LoginState;


        }
        //地區列表 for ajax
        [HttpPost]
        public ActionResult GetCountryList()
        {
            
            var result = ShareService.Instance.GetCountry();
            var list = result.Distinct(c => c.City).Select(c => c.City).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);

        }
        /// <summary>
        /// 第二層下拉選單 連動地區
        /// </summary>
        /// <param name="City"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetArea(string City)
        {
            
            var result = ShareService.Instance.GetCountry();
           var list = result.Where(c => c.City == City).Distinct(c => c.Area).Select(c => c.Area).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);

        }
        /// <summary>
        /// 訓練項目
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetTrainingProgramList()
        {
            
            var result = ShareService.Instance.GetTrainingProgram();
            return Json(result, JsonRequestBehavior.AllowGet);

        }
        /// <summary>
        /// 課程形式
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetCourseList()
        {
            
            var result = ShareService.Instance.GetCourse();
            return Json(result, JsonRequestBehavior.AllowGet);

        }
    }
}