using FitnessCenterAPI.Facade;
using FitnessCenterModel;
using FitnessCenterModel.DTO;
using FitnessCenterModel.Para;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FitnessCenterAPI.Controllers
{
    public class ReserveController : ApiController
    {
        ReserveFacade facade = new ReserveFacade();
        [HttpPost]
        public Result ReserveInterview(InteriewPara model)
        {
            return facade.ReserveInterview(model);

        }
        [HttpPost]
        public Result UpdateInterviewOrder(UpdateInterview model)
        {
            return facade.UpdateInterviewOrder(model);
        }
        [HttpPost]
        public Result ReserveClass(ServicePara model)
        {
            return facade.ReserveClass(model);
        }
        [HttpPost]
        public Result UpdateServiceOrder(UpdateServicePara model)
        {
            return facade.UpdateServiceOrder(model);
        }
        [HttpPost]
        public List<ReserveStatus> GetOrderStatus()
        {
            return facade.GetOrderStatus();
        }
        [HttpPost]
        public List<Interview> InterViewList(SearchServicePara input)
        {
            return facade.InterViewList(input);
        }
        [HttpPost]
        public List<CoachService> ServiceList(SearchServicePara input)
        {
            return facade.ServiceList(input);
        }
    }
}
