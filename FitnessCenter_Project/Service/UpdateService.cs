using FitnessCenterModel;
using FitnessCenterModel.DTO;
using FitnessCenterModel.Para;
using FitnessCenterModel.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FitnessCenter_Project.Service
{
    public class UpdateService
    {
        public UpdateMember GetMemberInfo(string MemberId)
        {
            var member = new Member();
            GetUserPara para = new GetUserPara() { MemberId = MemberId };
            
            string returnStr = ShareService.Instance.SendApi("Member/GetMemberInfo", JsonConvert.SerializeObject(para));
            member = JsonConvert.DeserializeObject<Member>(returnStr);
            UpdateMember updatemodel = new UpdateMember();
            updatemodel.OriginArea = member.Area;
            member.Area = null; //放null multiselect才可以繫結 否則頁面會抓不到資料
            updatemodel.User = member;
            
            return updatemodel;

        }
        public UpdateCoach GetCoachInfo(string MemberId) {
            var coach = new Coach();
            GetUserPara para = new GetUserPara() { MemberId = MemberId };
            
            string returnStr = ShareService.Instance.SendApi("Member/GetCoachInfo", JsonConvert.SerializeObject(para));
            coach = JsonConvert.DeserializeObject<Coach>(returnStr);

            UpdateCoach model = new UpdateCoach();
            model.OriginArea = coach.Area;//存原始資料 再清空
            model.OriginTrainProgram = coach.TrainingProgramId;
            model.OriginCourse = coach.CourseId;
            model.OriginExperience = coach.Experience;
            model.OriginLicense = coach.License;
            model.OriginCompetition = coach.Competiton;
            coach.Area = null;
            coach.TrainingProgramId = null;
            coach.CourseId = null;
            coach.Experience = null;
            coach.License = null;
            coach.Competiton = null;
            model.User = coach;
            return model;
        }
        
        //可以傳多個參數進來
        public IEnumerable<SelectListItem> GetCity(params string[] selecteds)
        {
            
            var result = ShareService.Instance.GetCountry();
            var list = result.Distinct(c => c.City)
                .Select(c => new SelectListItem() {
                    Text = c.City,
                    Value = c.City,
                    Selected=selecteds.Contains(c.City)
                })
                .ToList();
            
            return list;

        }
        public IEnumerable<SelectListItem> GetArea()
        {
            
            var result = ShareService.Instance.GetCountry();
            var list = result.Distinct(c => c.Area)
                .Select(c => new SelectListItem() { Text = c.Area, Value = c.Area })
                .ToList();
            return list;

        }
        public IEnumerable<SelectListItem> GetTrainingProgram(params int[] selecteds)
        {
            
            var result = ShareService.Instance.GetTrainingProgram();
            var list = result
                .Select(c => new SelectListItem()
                {
                    Text = c.TrainingProgram,
                    Value = Convert.ToString(c.ID),
                    Selected = selecteds.Contains(c.ID)
                })
                .ToList();
            return list;
        }
        public IEnumerable<SelectListItem> GetCourse(params int[] selecteds)
        {
            
            
            var result = ShareService.Instance.GetCourse();
            var list = result
                .Select(c => new SelectListItem()
                {
                    Text = c.Course,
                    Value = Convert.ToString(c.ID),
                    Selected = selecteds.Contains(c.ID)
                })
                .ToList();
            return list;

        }
        #region Update
        public Result UpdateMember(UpdateMemberViewModel model)
        {
            var result = new Result();
            
            string Msg = string.Empty;
            bool check = true;
            Member User = model.UpdateModel.User;
            
            if (!ShareService.Instance.CheckFormat(User.Phone, @"^[\d]*$", 10, ref Msg))
            {
                result.Message += $"手機{Msg}\r";
                check = false;
            }
            if (check == false)
            {
                result.ReturnNo = -1;
                return result;
            }
            User.Address = $"{model.addressCity}|{model.addressArea}|{User.Address}";
            //處理圖片
            if (model.Image != null && model.Image.ContentLength > 0)
            {
                //存到資料夾
                var FileName = User.MemberId + DateTime.Now.ToString("yyyyMMdd");
                var FilePath = Path.Combine(HttpContext.Current.Server.MapPath("~/MemberImage/"), FileName);
                model.Image.SaveAs(FilePath);
                User.Image = FileName;//存圖片檔名
            }
            //開始更新
            var input = new UpdateMember()
            {
                User = User,
                OriginArea = model.UpdateModel.OriginArea
            };

            string returnStr = ShareService.Instance.SendApi("Member/UpdateMember", JsonConvert.SerializeObject(input));
            result = JsonConvert.DeserializeObject<Result>(returnStr);

            return result;

        }
        public Result UpdateCoach(UpdateCoachViewModel model)
        {
            var result = new Result();
            
            
            string Msg = string.Empty;
            bool check = true;
            Coach User = model.UpdateModel.User;
            if (!ShareService.Instance.CheckFormat(User.Phone, @"^[\d]*$", 10, ref Msg))
            {
                result.Message += $"手機{Msg}\r";
                check = false;
            }
            if (check == false)
            {
                result.ReturnNo = -1;
                return result;
            }
            User.Address = $"{model.addressCity}|{model.addressArea}|{User.Address}";
            if (model.Image != null && model.Image.ContentLength > 0)
            {

                //存到資料夾
                var FileName = User.MemberId + DateTime.Now.ToString("yyyyMMdd");
                var FilePath = Path.Combine(HttpContext.Current.Server.MapPath("~/CoachImage/"), FileName);
                model.Image.SaveAs(FilePath);
                User.Image = FileName;
            }
           //要發送的model
            model.UpdateModel.User = User;
            var input = model.UpdateModel;
            string returnStr = ShareService.Instance.SendApi("Member/UpdateCoach", JsonConvert.SerializeObject(input));
            result = JsonConvert.DeserializeObject<Result>(returnStr);
            return result;
            
        }
        #endregion


    }
}