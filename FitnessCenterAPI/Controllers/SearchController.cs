using FitnessCenterAPI.Facade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FitnessCenterModel.DTO;
using FitnessCenterModel.Para;
namespace FitnessCenterAPI.Controllers
{
    public class SearchController : ApiController
    {
        SearchFacade facade = new SearchFacade();
        [HttpPost]
        public List<CoachData> CoachDataList(CoachSearchView model)
        {
            return facade.CoachDataList(model);
        }


    }
}
