using FitnessCenterModel;
using FitnessCenterModel.DTO;
using FitnessCenterModel.Para;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FitnessCenter_Project.Service
{
    public class ReserveService
    {

        #region 面談
        //預約面談
        public Result reserveInterview(InteriewPara para)
        {

            var result = new Result();
            string returnStr = ShareService.Instance.SendApi("Reserve/ReserveInterview", JsonConvert.SerializeObject(para));
            result = JsonConvert.DeserializeObject<Result>(returnStr);
            return result;
        }
        //更新面談訂單
        public Result UpdateInterviewOrder(UpdateInterview para)
        {
            var result = new Result();
            string returnStr = ShareService.Instance.SendApi("Reserve/UpdateInterviewOrder", JsonConvert.SerializeObject(para));
            result = JsonConvert.DeserializeObject<Result>(returnStr);
            return result;

        }



        //面談列表
        public List<Interview> SearchInterview(string MemberId, Status State)
        {
            SearchServicePara para = new SearchServicePara()
            {
                MemberId = MemberId,
                Type = State
            };
            var list = new List<Interview>();
            string returnStr = ShareService.Instance.SendApi("Reserve/InterViewList", JsonConvert.SerializeObject(para));
            list = JsonConvert.DeserializeObject<List<Interview>>(returnStr);
            return list;
        }
        #endregion

        #region 服務
        //預約服務
        public Result reserveService(ServicePara para)
        {

            var result = new Result();
            string returnStr = ShareService.Instance.SendApi("Reserve/ReserveSrvice", JsonConvert.SerializeObject(para));
            result = JsonConvert.DeserializeObject<Result>(returnStr);
            return result;
        }
        //更新服務訂單
        public Result UpdateServiceOrder(UpdateServicePara para)
        {
            var result = new Result();
            string returnStr = ShareService.Instance.SendApi("Reserve/UpdateServiceOrder", JsonConvert.SerializeObject(para));
            result = JsonConvert.DeserializeObject<Result>(returnStr);
            return result;

        }




        //服務列表
        public List<CoachService> SearchService(string MemberId, Status State)
        {
            SearchServicePara para = new SearchServicePara()
            {
                MemberId = MemberId,
                Type = State
            };
            var list = new List<CoachService>();
            string returnStr = ShareService.Instance.SendApi("Reserve/ServiceList", JsonConvert.SerializeObject(para));
            list = JsonConvert.DeserializeObject<List<CoachService>>(returnStr);
            return list;
        }
        #endregion


        /// <summary>
        /// 
        /// </summary>
        /// <param name="Status">帶入訂單資料的Status</param>
        /// <param name="types">面談[1,2,4,5,6] 服務[1,2,3,4,5,6]</param>
        /// <returns></returns>
        public IEnumerable<SelectListItem> OrderStatusList(int StatusId,params int[] types)
        {
            var result = ShareService.Instance.GetReserveStatus();
            var list = result.Where(c=> types.Contains(c.ID)).Select(c => new SelectListItem()
                {
                    Text = c.Status,
                    Value = Convert.ToString(c.ID),
                    Selected = StatusId.Equals(c.ID)
            })
                .ToList();

            return list;

        }
        //取教練有選的課程
        public IEnumerable<SelectListItem> GetCoachCourseList(params int[] types)
        {
            var result = ShareService.Instance.GetCourse();
            var list = result.Where(c=>types.Contains(c.ID))
                .Select(c => new SelectListItem() { Text = c.Course, Value = c.ID.ToString() })
               .ToList();

            return list;
        }
        public IEnumerable<SelectListItem> GetCoachTraingProgramList(params int[] types)
        {
            var result = ShareService.Instance.GetTrainingProgram();
            var list = result.Where(c => types.Contains(c.ID))
                .Select(c => new SelectListItem() { Text = c.TrainingProgram, Value = c.ID.ToString() })
               .ToList();
            
            return list;
        }
       
    }
}