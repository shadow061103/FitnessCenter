using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FitnessCenterAPI.Strategy;
using FitnessCenterModel.DTO;
using FitnessCenterModel;
using FitnessCenterAPI.Service;
using FitnessCenterAPI.Facade;
using FitnessCenterModel.Para;
using FitnessCenterModel.ViewModel;

namespace FitnessCenterAPI.Controllers
{
    public class MemberController : ApiController
    {
        #region 一般會員
        MemberFacade member = new MemberFacade();
        [HttpPost]
        public Member GetMemberInfo(GetUserPara input)
        {
            return member.GetMemberInfo(input.MemberId);
        }
        [HttpPost]
        public RegisterResult MemberRegister(Member User)
        {
            return member.Register(User);
        }
        [HttpPost]
        public LoginResult<User> MemberLogin(LoginModel model)
        {
            return member.Login(model.Email, model.Password);

        }
        [HttpPost]
        public Result UpdateMember(UpdateMember model)
        {
            return member.Update(model);
        }
        [HttpPost]
        public Result InsertMemberPhoto(UploadPhoto model)
        {
            return member.InsertUserPhoto(model.MemberId, model.ImageName);
        }
        [HttpPost]
        public Result UpdateMemberPhoto(UploadPhoto model)
        {
            return member.UpdateUserPhoto(model.MemberId, model.ImageName);
        }
        #endregion
        #region 教練會員
        CoachFacade coach = new CoachFacade();
        [HttpPost]
        public Coach GetCoachInfo(GetUserPara input)
        {
            return coach.GetMemberInfo(input.MemberId);
        }
        [HttpPost]
        public RegisterResult CoachRegister(Coach User)
        {
            return coach.Register(User);
        }
        [HttpPost]
        public LoginResult<User> CoachLogin(LoginModel model)
        {
            return coach.Login(model.Email, model.Password);

        }
        [HttpPost]
        public Result UpdateCoach(UpdateCoach model)
        {
            return coach.Update(model);
        }
        [HttpPost]
        public Result InsertCoachPhoto(UploadPhoto model)
        {
            return coach.InsertUserPhoto(model.MemberId, model.ImageName);
        }
        [HttpPost]
        public Result UpdateCoachPhoto(UploadPhoto model)
        {
            return coach.UpdateUserPhoto(model.MemberId, model.ImageName);
        }
        #endregion
    }
}
