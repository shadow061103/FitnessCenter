using FitnessCenterModel;
using FitnessCenterModel.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FitnessCenterService.Interface
{
    public interface IShareRepository
    {
        IList<TaiwanCountry> GetCountryList();
        IList<FitnessTrainingProgram> GetTrainingProgramList();
        IList<FitnessCourse> GetCourseList();


    }
}