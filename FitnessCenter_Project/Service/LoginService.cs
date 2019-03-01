using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FitnessCenterModel;
using FitnessCenterModel.DTO;
using Newtonsoft.Json;
using AutoMapper;

namespace FitnessCenter_Project.Service
{
    public class LoginService
    {
        public LoginResult<User> Login(LoginModel model)
        {
            var result = new LoginResult<User>();
            
            string Msg = string.Empty;
            bool check = true;
            if (!ShareService.Instance.CheckFormat(model.Email, @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", 50, ref Msg))
            {
                result.Message += $"帳號{Msg}\r";
                check = false;
            }
            if (!ShareService.Instance.CheckFormat(model.Password, @"^(?=.*[A-Za-z])(?=.*\d)[\w]{4,12}$", 50, ref Msg))
            {
                result.Message += $"密碼{Msg}\r";
                check = false;
            }
            if (check == false)
            {
                result.ReturnNo = -1;
                return result;
            }
            //驗證完畢
            var UserInfo = new User();
            
            if (model.State == 0)  //一般會員登入
            {
                string returnStr = ShareService.Instance.SendApi("Member/MemberLogin", JsonConvert.SerializeObject(model));
                result = JsonConvert.DeserializeObject<LoginResult<User>>(returnStr);
                
            }
            else //教練登入
            {
                string returnStr = ShareService.Instance.SendApi("Member/CoachLogin", JsonConvert.SerializeObject(model));
                result = JsonConvert.DeserializeObject<LoginResult<User>>(returnStr);
            }
            
            return result;
        }
    }
}