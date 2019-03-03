using FitnessCenterModel;
using FitnessCenterModel.DTO;
using FitnessCenterModel.Para;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace FitnessCenter_Project.Service
{
    public class SearchService
    {
        //地區搜尋
        public IEnumerable<SelectListItem> GetCountry()
        {
            
            var result = ShareService.Instance.GetCountry();
            var list = result.Distinct(c => c.City)
                .Select(c =>new SelectListItem() {Text= c.City,Value= c.City } )
                .ToList();
            list.Insert(0, new SelectListItem() { Text = "請選擇上課地區", Value = "" });
            return list;
            
        }
        //訓練項目搜尋
        public IEnumerable<SelectListItem> GetTrainingProgramList()
        {
            
            var result = ShareService.Instance.GetTrainingProgram();
            var list = result.Select(c => new SelectListItem() { Text = c.TrainingProgram, Value = c.ID.ToString() })
               .ToList();
            list.Insert(0, new SelectListItem() { Text = "請選擇訓練項目", Value = "0" });
            return list;
        }
        //課程形式
        public IEnumerable<SelectListItem> GetCourseList()
        {
            
            var result = ShareService.Instance.GetCourse();
            var list = result.Select(c => new SelectListItem() { Text = c.Course, Value = c.ID.ToString() })
               .ToList();
            list.Insert(0, new SelectListItem() { Text = "請選擇課程形式", Value = "0" });
            return list;
        }
        /// <summary>
        /// 教練搜尋
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<CoachData> GetCoachList(CoachSearchView input)
        {
            var result = new List<CoachData>();
            
            string returnStr = ShareService.Instance.SendApi("Search/CoachDataList", JsonConvert.SerializeObject(input));
            result = JsonConvert.DeserializeObject<List<CoachData>>(returnStr);
            return result;


        }
        /// <summary>
        /// 教練資料
        /// </summary>
        /// <param name="MemberId"></param>
        /// <returns></returns>
        public Coach GetCoachDetail(string MemberId)
        {
            var coach = new Coach();
            var input = new GetUserPara (){ MemberId = MemberId };
           
            string returnStr = ShareService.Instance.SendApi("Member/GetCoachInfo", JsonConvert.SerializeObject(input));
            coach = JsonConvert.DeserializeObject<Coach>(returnStr);
            return coach;


        }
        //地址轉換座標 for google map api
        public async System.Threading.Tasks.Task<Location> TransAddressToLocation(string Address)
        {

            if (string.IsNullOrEmpty(Address))
            {
                return null;
            }
            MapAddressData result = new MapAddressData();
            Address = Address.Replace("|", ""); //資料庫取出來的先做轉換
            string para = $"address={Address}&key=key";
            string url = "https://maps.googleapis.com/maps/api/geocode/json?" + para;

            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.PostAsync(url, null);
            string responseBody = await response.Content.ReadAsStringAsync();
            result = JsonConvert.DeserializeObject<MapAddressData>(responseBody);
            if (result.status == "OK")
            {
                return result.results[0].geometry.location;
            }
            else
                return null;
            
        }


    }
}