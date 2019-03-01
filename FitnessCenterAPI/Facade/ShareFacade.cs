using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FitnessCenterService.Interface;
using FitnessCenterService.Repository;
using FitnessCenterModel;
using FitnessCenterModel.DTO;

namespace FitnessCenterAPI.Facade
{
    public class ShareFacade
    {
        IShareRepository repository;
        public ShareFacade()
        {
            repository = new ShareRepository();
        }
        //台灣地區資料
        public List<TaiwanCountry> GetCountryList()
        {
            return repository.GetCountryList().ToList();
        }
        //訓練項目
        public List<FitnessTrainingProgram> GetTrainingProgramList()
        {
            return repository.GetTrainingProgramList().ToList();
        }
        //課程形式
        public List<FitnessCourse> GetCourseList()
        {
            return repository.GetCourseList().ToList();
        }
    }
}