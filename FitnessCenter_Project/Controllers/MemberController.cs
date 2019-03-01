using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FitnessCenterModel;
using FitnessCenter_Project.Service;
using FitnessCenterModel.DTO;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using System.Web.Security;
using FitnessCenterModel.ViewModel;
using System.IO;

namespace FitnessCenter_Project.Controllers
{
    public class MemberController : baseController
    {
        
        public ActionResult Register()
        {
            return View();
        }
        //ajax方法
        [HttpPost]
        public ActionResult RegisterMember(RegisterMebmberViewModel model)
        {
            RegisterSerivce service = new RegisterSerivce();
            RegisterResult result = service.RegisterMember(model);
            return Json(new { success = result.ReturnNo==1? true:false,ex=result.Message },
                 JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdateMember()
        {
            UpdateService service = new UpdateService();
            UpdateMemberViewModel model = new UpdateMemberViewModel();
            //檢查是否登入狀態
            if (LoginState)
            {
                string City = string.Empty;
                string Area = string.Empty;
                string Address = string.Empty;
                User user = GetMemberInfo();
                string MemberId = user.MemberId;

                UpdateMember updatemodel = service.GetMemberInfo(MemberId);
                string[] AddressArray = updatemodel.User.Address.Split('|');
                if (AddressArray.Length == 3)
                {
                     City = AddressArray[0];
                     Area = AddressArray[1];
                    updatemodel.User.Address = AddressArray[2];
                }
                
                
                model.addressCity = City;
                model.addressArea = Area;
                model.addressCityList = new SelectList(service.GetCity(City), "Value", "Text");
                model.addresAreaList = new SelectList(service.GetArea(), "Value", "Text", Area);
                model.AreaList = new MultiSelectList(service.GetCity(updatemodel.OriginArea.ToArray()), "Value", "Text", updatemodel.OriginArea);
                model.UpdateModel = updatemodel;

            }
            else
               return RedirectToAction("Login", "Home");


            return View(model);

        }
        //ajax方法
        [HttpPost]
        public ActionResult UpdateMember(UpdateMemberViewModel model)
        {
            UpdateService service = new UpdateService();
            Result result = service.UpdateMember(model);

            return Json(new { success = result.ReturnNo == 1 ? true : false, ex = result.Message },
                  JsonRequestBehavior.AllowGet);
        }

        



    }
}