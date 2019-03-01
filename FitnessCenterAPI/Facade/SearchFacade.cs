using FitnessCenterAPI.Strategy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FitnessCenterModel.DTO;
using FitnessCenterModel.Para;
using FitnessCenterAPI.Service;
using FitnessCenterAPI.Factory;
using FitnessCenterService.Interface;
using FitnessCenterService.Repository;

namespace FitnessCenterAPI.Facade
{
    public class SearchFacade
    {
        ISearchRepository repository;
        public SearchFacade()
        {
            repository = GenericFactory.CreateInastance<ISearchRepository>(typeof(SearchRepository));
        }
        public List<CoachData> CoachDataList(CoachSearchView input)
        {
            var result = new List<CoachData>();
            try
            {
                //取整包資料
                var list = repository.ViewCoachData(input);

                var coachs = list.Select(c=>c.MemberId).Distinct();
                CoachData data = new CoachData();
                foreach (var coach in coachs)
                {


                    data = list.Where(c => c.MemberId == coach).Select(c => new CoachData()
                    {
                        MemberId=c.MemberId,
                        ImageName=c.ImageName,
                        Name=c.Name,
                        NickName=c.NickName,
                        Intoduction=c.Intoduction,
                        Email=c.Email

                    }).FirstOrDefault();
                    data.TrainingProgram = list.Where(c => c.MemberId == coach)
                        .Select(c=>c.TrainingProgram).Distinct().ToList();
                    data.Course= list.Where(c => c.MemberId == coach)
                        .Select(c=>c.Course).Distinct().ToList();
                    data.Area = list.Where(c => c.MemberId == coach)
                        .Select(c => c.Area).Distinct().ToList();
                    
                    result.Add(data);
                }


            }
            catch (Exception ex)
            {
                LogService.LogTxt("CoachDataList例外錯誤" + ex.ToString());
                return null;
            }
            return result;

        }
    }
}