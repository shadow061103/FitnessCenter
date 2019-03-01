using FitnessCenter_Project.Service;
using FitnessCenterModel;
using FitnessCenterModel.DTO;
using FitnessCenterModel.Para;
using FitnessCenterModel.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FitnessCenter_Project.Controllers
{
    public class ReserveController : baseController
    {
        //預約面談
        public ActionResult ReserveInterveiw(string CoachId,string CoachName)
        {
            ReserveInterviewModel model = new ReserveInterviewModel();
            if (LoginState)
            {
                User member = GetMemberInfo();
                InteriewPara para = new InteriewPara()
                {
                    CoachId = CoachId,
                    MemberId = member.MemberId,
                    ReserveDate = DateTime.Now
                };
                model.Para = para;
                model.CoachName = CoachName;
                return View(model);
            }
            else
                return RedirectToAction("Login", "Home");
            
                
        }
        //ajax
        [HttpPost]
        public ActionResult ReserveInterveiw(ReserveInterviewModel model)
        {
            ReserveService service = new ReserveService();
            var result = service.reserveInterview(model.Para);
            return Json(new { success = result.ReturnNo == 1 ? true : false, ex = result.Message },
                 JsonRequestBehavior.AllowGet);
        }
        //預約服務
        public ActionResult ReserveService(string CoachId,string CoachName)
        {
            ReserveServiceModel model = new ReserveServiceModel();
            ReserveService service = new ReserveService();
            if (LoginState)
            {
                User member = GetMemberInfo();
                SearchService search = new SearchService();
                Coach coach = search.GetCoachDetail(CoachId);
                ServicePara para = new ServicePara()
                {
                    CoachId = CoachId,
                    MemberId = member.MemberId,
                    ServiceDate = DateTime.Now,
                    Location = coach.Address.Replace("|", "")//預設教練地址
                };
                model.Para = para;
                model.CoachName = CoachName;
                
                //教練有服務的項目
                var trainprogrem= service.GetCoachTraingProgramList(coach.TrainingProgramId.ToArray());
                var course = service.GetCoachCourseList(coach.CourseId.ToArray());
                model.TrainProgramList = new SelectList(trainprogrem, "Value", "Text");
                model.CourseList = new SelectList(course, "Value", "Text");
                return View(model);
            }
            else
                return RedirectToAction("Login", "Home");




            
        }
        [HttpPost]
        public ActionResult ReserveService(ReserveServiceModel model)
        {
            ReserveService service = new ReserveService();
            var result = service.reserveService(model.Para);
            return Json(new { success = result.ReturnNo == 1 ? true : false, ex = result.Message },
                 JsonRequestBehavior.AllowGet);

        }

        //預約服務/面談 檢視
        public ActionResult OrderList()
        {
            ReserveService service = new ReserveService();
            if (LoginState)
            {
                User member = GetMemberInfo();
                OrderListModel model = new OrderListModel() {
                    inserview= service.SearchInterview(member.MemberId,member.State),
                    service=service.SearchService(member.MemberId, member.State),
                    State=member.State
                };



                return View(model);
            }
            else
                return RedirectToAction("Login", "Home");

            
        }
        #region 檢視面談
        //教練檢視
        public ActionResult CoachInterViewDetail(int OrderId=1)
        {
            ReserveService service = new ReserveService();
            int[] status =new int[] { 1, 2, 4, 5, 6 };//DB定義的Status ID
            if (LoginState)
            {
                User member = GetMemberInfo();
                Interview interview = service.SearchInterview(member.MemberId, member.State)
                    .Where(c=>c.OrderId.Equals(OrderId)).FirstOrDefault();
                var statusList= service.OrderStatusList(interview.StatusId, status);
               var model = new InterViewDetailModel()
                {
                    StatusList = new SelectList(statusList, "Value", "Text"),
                    interview = interview,
                   
               };
                return View(model);
            }
            else
                return RedirectToAction("Login", "Home");
            
        }
        //會員檢視
        public ActionResult MemberInterViewDetail(int OrderId=1)
        {
            ReserveService service = new ReserveService();
            int[] types = new int[] { 1, 2, 4, 5};
            if (LoginState)
            {
                User member = GetMemberInfo();
                
                Interview interview = service.SearchInterview(member.MemberId, member.State)
                    .Where(c => c.OrderId.Equals(OrderId)).FirstOrDefault();
                var status = ShareService.Instance.GetReserveStatus().Where(c => types.Contains(c.ID)).ToList();
                InterviewDetail model = new InterviewDetail() {
                    interview=interview,
                    Status= status

                };

                return View(model);
            }
            else
                return RedirectToAction("Login", "Home");
            
        }
        [HttpPost]
        public ActionResult UpdateInterview(UpdateInterview para)
        {
            ReserveService service = new ReserveService();
            var result = service.UpdateInterviewOrder(para);
            return Json(new { success = result.ReturnNo == 1 ? true : false, ex = result.Message },
                 JsonRequestBehavior.AllowGet);

        }
        #endregion

        #region 檢視服務
        //教練檢視
        public ActionResult CoachServiceDatail(int OrderId=5)
        {
            ReserveService service = new ReserveService();
            int[] status = new int[] { 1, 2, 3,4, 5, 6 };//DB定義的Status ID
            if (LoginState)
            {
                User member = GetMemberInfo();
                CoachService reserve = service.SearchService(member.MemberId, member.State)
                    .Where(c => c.OrderId.Equals(OrderId)).FirstOrDefault();
                var statusList = service.OrderStatusList(reserve.StatusId, status);
                var model = new ServiceDetailModel()
                {
                    StatusList = new SelectList(statusList, "Value", "Text"),
                     Service= reserve

                };
                return View(model);
            }
            else
                return RedirectToAction("Login", "Home");
            
        }
        //會員檢視
        public ActionResult MemberServiceDetail(int OrderId=5)
        {
            ReserveService service = new ReserveService();
            int[] types = new int[] { 1, 2, 3,4, 5,6 };
            if (LoginState)
            {
                User member = GetMemberInfo();

                CoachService reserve = service.SearchService(member.MemberId, member.State)
                    .Where(c => c.OrderId.Equals(OrderId)).FirstOrDefault();
                var status = ShareService.Instance.GetReserveStatus().Where(c => types.Contains(c.ID)).ToList();
                ServiceDetail model = new ServiceDetail()
                {
                    Service = reserve,
                    Status = status

                };

                return View(model);
            }
            else
                return RedirectToAction("Login", "Home");




        }
        [HttpPost]
        public ActionResult UpdateService(UpdateServicePara para)
        {
            ReserveService service = new ReserveService();
            var result = service.UpdateServiceOrder(para);
            return Json(new { success = result.ReturnNo == 1 ? true : false, ex = result.Message },
                 JsonRequestBehavior.AllowGet);
            
        }

        #endregion
    }
}