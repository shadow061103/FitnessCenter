using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FitnessCenterModel;
using FitnessCenterModel.DTO;
using Newtonsoft.Json;
using FitnessCenterModel.ViewModel;
using System.IO;

namespace FitnessCenter_Project.Service
{
    public class RegisterSerivce
    {
        public RegisterResult RegisterMember(RegisterMebmberViewModel model)
        {
            var result = new RegisterResult();
            
            string Msg = string.Empty;
            bool check = true;
            Member User = model.User;
            if (!ShareService.Instance.CheckFormat(User.Email, @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", 50, ref Msg))
            {
                result.Message += $"帳號{Msg}\r";
                check = false;
            }
            if (!ShareService.Instance.CheckFormat(User.Password, @"^(?=.*[A-Za-z])(?=.*\d)[\w]{4,12}$", 50, ref Msg))
            {
                result.Message += $"密碼{Msg}\r";
                check = false;
            }
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
            //把地址地區用|隔開
            User.Address = $"{model.addressCity}|{model.addressArea}|{User.Address}";

            //註冊
            string returnStr = ShareService.Instance.SendApi("Member/MemberRegister", JsonConvert.SerializeObject(User));
            result = JsonConvert.DeserializeObject<RegisterResult>(returnStr);



           
            //註冊成功 上傳大頭照
            if (result.ReturnNo == 1)
            {
                
                if (model.Image!=null && model.Image.ContentLength>0)
                {
                    //存到資料夾
                    var FileName = result.MemberId + DateTime.Now.ToString("yyyyMMdd")+".jpg";
                    var FilePath = Path.Combine(HttpContext.Current.Server.MapPath("~/MemberImage/"), FileName);
                    model.Image.SaveAs(FilePath);
                    
                    var imagemodel = new UploadPhoto() { MemberId= result.MemberId, ImageName = FileName };
                    returnStr = ShareService.Instance.SendApi("Member/InsertMemberPhoto", JsonConvert.SerializeObject(imagemodel));
                   var imageResult = JsonConvert.DeserializeObject<Result>(returnStr);
                    if (imageResult.ReturnNo != 1)
                    {
                        result.ReturnNo = -99;
                        result.Message = "上傳圖片失敗" + imageResult.Message;
                    }

                }
            }
            
            return result;

        }
        public RegisterResult RegisterCoach(RegisterCoachViewModel model)
        {
            var result = new RegisterResult();
            
            string Msg = string.Empty;
            bool check = true;
            Coach User = model.User;
            if (!ShareService.Instance.CheckFormat(User.Email, @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", 50, ref Msg))
            {
                result.Message += $"帳號{Msg}\r";
                check = false;
            }
            if (!ShareService.Instance.CheckFormat(User.Password, @"^(?=.*[A-Za-z])(?=.*\d)[\w]{4,12}$", 50, ref Msg))
            {
                result.Message += $"密碼{Msg}\r";
                check = false;
            }
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
            //註冊
            string returnStr = ShareService.Instance.SendApi("Member/CoachRegister", JsonConvert.SerializeObject(User));
            result = JsonConvert.DeserializeObject<RegisterResult>(returnStr);
            
            //註冊成功 上傳大頭照
            if (result.ReturnNo == 1)
            {


                
                if (model.Image != null && model.Image.ContentLength > 0)
                {

                    //存到資料夾
                    var FileName = result.MemberId + DateTime.Now.ToString("yyyyMMdd") + ".jpg"; ;
                    var FilePath = Path.Combine(HttpContext.Current.Server.MapPath("~/CoachImage/"), FileName);
                    model.Image.SaveAs(FilePath);
                    
                    var imagemodel = new UploadPhoto() { MemberId = result.MemberId, ImageName = FileName };
                    returnStr = ShareService.Instance.SendApi("Member/InsertCoachPhoto", JsonConvert.SerializeObject(imagemodel));
                    var imageResult = JsonConvert.DeserializeObject<Result>(returnStr);
                    if (imageResult.ReturnNo != 1)
                    {
                        result.ReturnNo = -99;
                        result.Message = "上傳圖片失敗" + imageResult.Message;
                    }

                }
            }
            return result;

        }
    }
}