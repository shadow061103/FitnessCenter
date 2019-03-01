using FitnessCenterModel.DTO;
using FitnessCenterModel.Para;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FitnessCenterService.Interface
{
    public interface ISearchRepository
    {
        List<CoachView> ViewCoachData(CoachSearchView input);
    }
}