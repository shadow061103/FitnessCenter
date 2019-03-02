using FitnessCenterModel;
using FitnessCenterModel.DTO;
using FitnessCenterModel.Para;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FitnessCenterService.Interface
{
    public interface IReserveRepository
    {
        List<ReserveStatus> GetOrderStatus();
        Result ReserveInterview(InteriewPara model);
        Result ReserveClass(ServicePara model);
        List<Interview> InterViewList(string MemberId, int Type);
        List<CoachService> ServiceList(string MemberId, int Type);
        Result UpdateInterviewOrder(UpdateInterview model);
        Result UpdateServiceOrder(UpdateServicePara model);
    }
}