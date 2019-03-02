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
            
            var result = ReserveService.Instance.reserveInterview(model.Para);
            return Json(new { success = result.ReturnNo == 1 ? true : false, ex = result.Message },
                 JsonRequestBehavior.AllowGet);
        }
        //預約服務
        public ActionResult ReserveClass(string CoachId,string CoachName)
        {
            ReserveServiceModel model = new ReserveServiceModel();
            
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
                var trainprogrem= ReserveService.Instance.GetCoachTraingProgramList(coach.TrainingProgramId.ToArray());
                var course = ReserveService.Instance.GetCoachCourseList(coach.CourseId.ToArray());
                model.TrainProgramList = new SelectList(trainprogrem, "Value", "Text");
                model.CourseList = new SelectList(course, "Value", "Text");
                return View(model);
            }
            else
                return RedirectToAction("Login", "Home");




            
        }
        [HttpPost]
        public ActionResult ReserveClass(ReserveServiceModel model)
        {
            
            var result = ReserveService.Instance.reserveClass(model.Para);
            return Json(new { success = result.ReturnNo == 1 ? true : false, ex = result.Message },
                 JsonRequestBehavior.AllowGet);

        }

        //預約服務/面談 檢視
        public ActionResult OrderList()
        {
            
            if (LoginState)
            {
                User member = GetMemberInfo();
                OrderListModel model = new OrderListModel() {
                    inserview= ReserveService.Instance.SearchInterview(member.MemberId,member.State),
                    service= ReserveService.Instance.SearchService(member.MemberId, member.State),
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
            
            int[] status =new int[] { 1, 2, 4, 6};//DB定義的Status ID
            string[] disabled = new string[] { "6" };
            if (LoginState)
            {
                User member = GetMemberInfo();
                Interview interview = ReserveService.Instance.SearchInterview(member.MemberId, member.State)
                    .Where(c=>c.OrderId.Equals(OrderId)).FirstOrDefault();
                var statusList= ReserveService.Instance.OrderStatusList(interview.StatusId, status);
               var model = new InterViewDetailModel()
                {
                    StatusList = new SelectList(statusList, "Value", "Text", interview.StatusId, disabled),
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
            
            int[] types = new int[] { 1, 2, 4, 6};
            if (LoginState)
            {
                User member = GetMemberInfo();
                
                Interview interview = ReserveService.Instance.SearchInterview(member.MemberId, member.State)
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
        //更新面談狀態
        [HttpPost]
        public ActionResult UpdateInterview(UpdateInterview para)
        {
            
            var result = ReserveService.Instance.UpdateInterviewOrder(para);
            return Json(new { success = result.ReturnNo == 1 ? true : false, ex = result.Message },
                 JsonRequestBehavior.AllowGet);

        }
        #endregion

        #region 檢視服務
        //教練檢視
        public ActionResult CoachServiceDatail(int OrderId=5)
        {
            
            int[] status = new int[] { 1, 2, 3,4, 6 };//DB定義的Status ID
            string[] disabled = new string[] { "3","6" };
            if (LoginState)
            {
                User member = GetMemberInfo();
                CoachService reserve = ReserveService.Instance.SearchService(member.MemberId, member.State)
                    .Where(c => c.OrderId.Equals(OrderId)).FirstOrDefault();
                var statusList = ReserveService.Instance.OrderStatusList(reserve.StatusId, status);
                statusList.Where(c => c.Value == "3").FirstOrDefault().Disabled = true;
                var model = new ServiceDetailModel()
                {
                    StatusList = new SelectList(statusList, "Value", "Text",reserve.StatusId, disabled),
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
            
            int[] types = new int[] { 1, 2, 3,4,6 };
            if (LoginState)
            {
                User member = GetMemberInfo();

                CoachService reserve = ReserveService.Instance.SearchService(member.MemberId, member.State)
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
            
            var result = ReserveService.Instance.UpdateServiceOrder(para);
            return Json(new { success = result.ReturnNo == 1 ? true : false, ex = result.Message },
                 JsonRequestBehavior.AllowGet);
            
        }

        #endregion
    }
}