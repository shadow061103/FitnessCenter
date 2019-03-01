using FitnessCenterAPI.Factory;
using FitnessCenterModel;
using FitnessCenterModel.DTO;
using FitnessCenterService.Interface;
using FitnessCenterService.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FitnessCenterAPI.Strategy
{
    public class CoachStrategy //K:Repository T Member or Coach
    {
        ICoachRepository<Coach> Repository;
        public CoachStrategy()
        {

            Repository = GenericFactory.CreateInastance<ICoachRepository<Coach>>(typeof(CoachRepository));
        }
        public bool CheckifExist(string Account)
        {
            return Repository.CheckifExist(Account);
        }
        public Coach GetMemberInfo(string Account)
        {
            return Repository.GetMemberInfo(Account);
        }
        public Result Register(Coach User)
        {
            return Repository.Register(User);

        }
        public LoginResult<User> Login(string Account, string Password)
        {
            return Repository.Login(Account, Password);
        }
        public Result Update(Coach User)
        {
            return Repository.Update(User);
        }
        public Result InsertUserPhoto(string MemberId, string ImageName)
        {
            return Repository.InsertPhoto(MemberId, ImageName);
        }
        public Result UpdateUserPhoto(string MemberId, string ImageName)
        {
            return Repository.UpdatePhoto(MemberId, ImageName);
        }
        public string GetUserImage(string MemberId)
        {
            return Repository.GetUserImage(MemberId);
        }
        public Result InsertUserArea(string MemberId, string Area)
        {
            return Repository.InsertUserArea(MemberId, Area);
        }
        public Result DeleteUserArea(string MemberId,string Area)
        {
            return Repository.DeleteUserArea(MemberId, Area);
        }
        public List<string> GetUserArea(string MemberId)
        {
            return Repository.GetUserArea(MemberId);
        }
        public Result InsertTrainingProgram(string MemberId, int TrainingProgram)
        {
            return Repository.InsertTrainingProgram(MemberId, TrainingProgram);

        }
        public Result DeleteTrainingProgram(string MemberId, int TrainingProgram)
        {
            return Repository.DeleteTrainingProgram(MemberId, TrainingProgram);
        }
        public List<CoachTrainingProgram> GetTrainingProgram(string MemberId)
        {
            return Repository.GetTrainingProgram(MemberId);
        }
        public Result InsertCoachCourse(string MemberId, int Course)
        {
            return Repository.InsertCoachCourse(MemberId, Course);
        }
        public Result DeleteCoachCourse(string MemberId, int Course)
        {
            return Repository.DeleteCoachCourse(MemberId, Course);
        }
       public List<CoachCourse> GetCoachCourse(string MemberId)
        {
            return Repository.GetCoachCourse(MemberId);
        }
        public Result InsertCoachLicense(string MemberId, string License)
        {
            return Repository.InsertCoachLicense(MemberId, License);
        }
        public Result DeleteCoachLicense(string MemberId, string License)
        {
            return Repository.DeleteCoachLicense(MemberId, License);
        }
        public List<string> GetCoachLicense(string MemberId)
        {
            return Repository.GetCoachLicense(MemberId);
        }
        public Result InsertCoachExperience(string MemberId, string Experience)
        {
            return Repository.InsertCoachExperience(MemberId, Experience);
        }
        public Result DeleteCoachExperience(string MemberId, string Experience)
        {
            return Repository.DeleteCoachExperience(MemberId, Experience);
        }
        public List<string> GetCoachExperience(string MemberId)
        {
            return Repository.GetCoachExperience(MemberId);
        }
        public Result InsertCoachCompetiton(string MemberId, string Competiton)
        {
            return Repository.InsertCoachCompetiton(MemberId, Competiton);
        }
        public Result DeleteCoachCompetiton(string MemberId, string Competiton)
        {
            return Repository.DeleteCoachCompetiton(MemberId, Competiton);
        }
        public List<string> GetCoachCompetiton(string MemberId)
        {
            return Repository.GetCoachCompetiton(MemberId);
        }
    }
}