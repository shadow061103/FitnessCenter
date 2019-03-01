using FitnessCenterAPI.Factory;
using FitnessCenterModel;
using FitnessCenterModel.DTO;
using FitnessCenterModel.Para;
using FitnessCenterService.Interface;
using FitnessCenterService.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FitnessCenterAPI.Facade
{
    public class ReserveFacade
    {
        IReserveRepository repository;
        public ReserveFacade() {
            repository= GenericFactory.CreateInastance<IReserveRepository>(typeof(ReserveRepository));
        }
       
        public Result ReserveInterview(InteriewPara model)
        {
            return repository.ReserveInterview(model);

        }
        public Result UpdateInterviewOrder(UpdateInterview model)
        {
            return repository.UpdateInterviewOrder(model);
        }
        public Result ReserveSrvice(ServicePara model)
        {
            return repository.ReserveSrvice(model);
        }
        public Result UpdateServiceOrder(UpdateServicePara model)
        {
            return repository.UpdateServiceOrder(model);
        }
        public List<ReserveStatus> GetOrderStatus()
        {
            return repository.GetOrderStatus();
        }
        public List<Interview> InterViewList(SearchServicePara input)
        {
            return repository.InterViewList(input.MemberId, (int)input.Type);
        }
        public List<CoachService> ServiceList(SearchServicePara input)
        {
            return repository.ServiceList(input.MemberId, (int)input.Type);
        }
    }
}