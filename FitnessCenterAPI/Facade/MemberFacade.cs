using FitnessCenterAPI.Service;
using FitnessCenterAPI.Strategy;
using FitnessCenterModel;
using FitnessCenterModel.DTO;
using FitnessCenterModel.ViewModel;
using FitnessCenterService.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FitnessCenterAPI.Facade
{
    //會員中心 的方法
    public class MemberFacade
    {
        MemberStrategy strategy;
        public MemberFacade()
        {
            strategy = new MemberStrategy();
        }
        public Member GetMemberInfo(string MemberId)
        {
            Member member = strategy.GetMemberInfo(MemberId);
            member.Area = strategy.GetUserArea(MemberId);
            member.Image = strategy.GetUserImage(MemberId);
            return member;
        }
        public RegisterResult Register(Member User)
        {
            var result = new RegisterResult();
            var returnResult = new Result();
            if (strategy.CheckifExist(User.Email))
            {
                result.ReturnNo = -99;
                result.Message = "已有此帳號";
                return result;
            }
            else
            {
                try
                {
                    DecryptService service = new DecryptService();
                    User.Password = service.sha256(User.Password);
                    User.MemberId = Guid.NewGuid().ToString().Replace("-", "");
                    returnResult = strategy.Register(User);
                    if (returnResult.ReturnNo == 1) //基本資料新增完 新增上課地區
                    {
                        foreach (var area in User.Area)
                        {
                            returnResult = strategy.InsertUserArea(User.MemberId, area);
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
        public Result Update(UpdateMember model)
        {
            var result = new Result();

            try
            {

                result = strategy.Update(model.User);
                if (result.ReturnNo == 1)
                {

                    //相互取差集 要新增或刪掉的部分
                    List<string> insert = model.User.Area.Except(model.OriginArea).ToList();
                    List<string> delete = model.OriginArea.Except(model.User.Area).ToList();

                    foreach (var area in insert)
                    {
                        result = strategy.InsertUserArea(model.User.MemberId, area);
                    }
                    foreach (var area in delete)
                    {
                        result = strategy.DeleteUserArea(model.User.MemberId, area);
                    }
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