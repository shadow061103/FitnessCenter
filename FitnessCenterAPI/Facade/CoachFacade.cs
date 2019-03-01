using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FitnessCenterAPI.Service;
using FitnessCenterAPI.Strategy;
using FitnessCenterModel;
using FitnessCenterModel.DTO;
using FitnessCenterService.Repository;
using FitnessCenterModel.ViewModel;
using System.Reflection;

namespace FitnessCenterAPI.Facade
{
    public class CoachFacade
    {
        CoachStrategy strategy;
        Object obj; //反射用 避免重複建實體
        public CoachFacade()
        {
            strategy = new CoachStrategy();
            obj = AppDomain.CurrentDomain.CreateInstanceAndUnwrap("FitnessCenterAPI", "FitnessCenterAPI.Strategy.CoachStrategy");
        }
        public Coach GetMemberInfo(string MemberId)
        {
            Coach coach = strategy.GetMemberInfo(MemberId);
            coach.Area = strategy.GetUserArea(MemberId);
            coach.CourseId = strategy.GetCoachCourse(MemberId).Select(c => c.CourseId).ToList();
            coach.TrainingProgramId = strategy.GetTrainingProgram(MemberId).Select(c => c.TrainingProgramId).ToList();
            coach.Course= strategy.GetCoachCourse(MemberId).Select(c => c.Course).ToList();
            coach.TrainingProgram= strategy.GetTrainingProgram(MemberId).Select(c => c.TrainingProgram).ToList();
            coach.License = strategy.GetCoachLicense(MemberId);
            coach.Experience = strategy.GetCoachExperience(MemberId);
            coach.Competiton = strategy.GetCoachCompetiton(MemberId);
            coach.Image = strategy.GetUserImage(MemberId); //圖片檔名
            return coach;
        }
        public RegisterResult Register(Coach User)
        {
            var result = new RegisterResult();
            var returnResult = new Result();
            if (strategy.CheckifExist(User.Email))
            {
                result.ReturnNo = -99;
                result.Message = "已有此帳號";
                return result;
            }

            try
            {
                DecryptService service = new DecryptService();
                User.Password = service.sha256(User.Password);
                User.MemberId = Guid.NewGuid().ToString().Replace("-", "");
                //註冊基本資料
                returnResult = strategy.Register(User);
                
                if (returnResult.ReturnNo == 1)
                {//這裡有bug 要判斷是否有值

                    //上課地區
                    foreach (var area in User.Area)
                    {
                        returnResult = strategy.InsertUserArea(User.MemberId, area);
                    }
                    //訓練項目 代號
                    foreach (var program in User.TrainingProgramId)
                    {
                        returnResult = strategy.InsertTrainingProgram(User.MemberId, program);
                    }
                    //課程形式 代號
                    foreach (var course in User.CourseId)
                    {
                        returnResult = strategy.InsertCoachCourse(User.MemberId, course);
                    }
                    //證照
                    foreach (var license in User.License)
                    {
                        returnResult = strategy.InsertCoachLicense(User.MemberId, license);
                    }
                    //經歷
                    foreach (var item in User.Experience)
                    {
                        returnResult = strategy.InsertCoachExperience(User.MemberId, item);
                    }
                    //比賽經驗
                    foreach (var item in User.Competiton)
                    {
                        returnResult = strategy.InsertCoachCompetiton(User.MemberId, item);
                    }

                }
            }
            catch (Exception ex)
            {
                LogService.LogTxt("註冊例外錯誤" + ex.ToString());
                result.ReturnNo = -99;
                result.Message = "註冊例外錯誤";
                return result;
            }


            result.ReturnNo = 1;
            result.Message = "註冊成功";
            result.MemberId = User.MemberId;
            return result;

        }
       
        public LoginResult<User> Login(string Account, string Password)
        {
            var result = new LoginResult<User>();
            try
            {
                DecryptService service = new DecryptService();
                return strategy.Login(Account, service.sha256(Password));
            }
            catch (Exception ex)
            {
                LogService.LogTxt("登入例外錯誤" + ex.ToString());
                result.ReturnNo = -99;
                result.Message = "登入失敗";
            }
            return result;


        }
        public Result Update(UpdateCoach model)
        {
            var result = new Result();
            
                try
                {

                result = strategy.Update(model.User);
                if (result.ReturnNo == 1)
                {
                    //地區 取差集看要新增或刪掉
                    result = LoopReflectionOpera(ref result,"InsertUserArea", model.User.MemberId, model.User.Area, model.OriginArea);
                    result = LoopReflectionOpera(ref result, "DeleteUserArea", model.User.MemberId, model.OriginArea, model.User.Area);
                    //課程
                    result = LoopReflectionOpera(ref result, "InsertCoachCourse", model.User.MemberId, model.User.CourseId, model.OriginCourse);
                    result = LoopReflectionOpera(ref result, "DeleteCoachCourse", model.User.MemberId, model.OriginCourse, model.User.CourseId);
                    //訓練
                    result = LoopReflectionOpera(ref result, "InsertTrainingProgram", model.User.MemberId, model.User.TrainingProgramId, model.OriginTrainProgram);
                    result = LoopReflectionOpera(ref result, "DeleteTrainingProgram", model.User.MemberId, model.OriginTrainProgram, model.User.TrainingProgramId);
                    //證照
                    result = LoopReflectionOpera(ref result, "InsertCoachLicense", model.User.MemberId, model.User.License, model.OriginLicense);
                    result = LoopReflectionOpera(ref result, "DeleteCoachLicense", model.User.MemberId, model.OriginLicense, model.User.License);
                    //經驗
                    result = LoopReflectionOpera(ref result, "InsertCoachExperience", model.User.MemberId, model.User.Experience, model.OriginExperience);
                    result = LoopReflectionOpera(ref result, "DeleteCoachExperience", model.User.MemberId, model.OriginExperience, model.User.Experience);
                    //比賽
                    result = LoopReflectionOpera(ref result, "InsertCoachCompetiton", model.User.MemberId, model.User.Competiton, model.OriginCompetition);
                    result = LoopReflectionOpera(ref result, "DeleteCoachCompetiton", model.User.MemberId, model.OriginCompetition, model.User.Competiton);
                    


                }
                //更新照片
                if (!string.IsNullOrEmpty(model.User.Image))
                {
                    result = UpdateUserPhoto(model.User.MemberId, model.User.Image);
                }


            }
                catch (Exception ex)
                {
                    LogService.LogTxt("更新資料例外錯誤" + ex.ToString());
                    result.ReturnNo = -99;
                    result.Message = "更新資料例外錯誤";
                }

            
            return result;

        }
        //批次新增刪除
        /// <summary>
        /// 使用反射簡化foreach 太多的問題
        /// </summary>
        /// <typeparam name="T">List的型別</typeparam>
        /// <param name="result"></param>
        /// <param name="functionName">CoachStrategy的方法名稱</param>
        /// <param name="MemberId">教練ID</param>
        /// <param name="Sourse1"></param>
        /// <param name="Sourse2"></param>
        /// <returns></returns>
        public Result LoopReflectionOpera<T>(ref Result result, string functionName, string MemberId, List<T> Sourse1, List<T> Sourse2)
        {
           
            
            Type[] types = new Type[] { typeof(String), typeof(T) };
            MethodInfo method = obj.GetType().GetMethod(functionName, types);//找符合條件的function
            foreach (T item in Sourse1.Except(Sourse2)) //Sourse1有Sourse2沒有 取差集判斷新增或刪除
            {
                result = (Result)method.Invoke(obj, new object[] { MemberId, item }); //執行
                if (result.ReturnNo != 1)
                {
                    LogService.LogTxt($"Function:{functionName},Message:{result.Message},{MemberId},{item}");
                    continue;
                }
            }

            return result;

        }
        public Result InsertUserPhoto(string MemberId, string ImageName)
        {
            return strategy.InsertUserPhoto(MemberId, ImageName);
        }
        public Result UpdateUserPhoto(string MemberId, string ImageName)
        {
            return strategy.UpdateUserPhoto(MemberId, ImageName);
        }
    }
}