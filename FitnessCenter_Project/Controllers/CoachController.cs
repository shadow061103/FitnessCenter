using FitnessCenter_Project.Service;
using FitnessCenterModel;
using FitnessCenterModel.DTO;
using FitnessCenterModel.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FitnessCenter_Project.Controllers
{
    public class CoachController : baseController
    {
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult RegisterCoach(RegisterCoachViewModel model)
        {
            RegisterSerivce service = new RegisterSerivce();
            RegisterResult result = service.RegisterCoach(model);
            return Json(new { success = result.ReturnNo == 1 ? true : false, ex = result.Message },
                             JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateCoach()
        {
            UpdateService service = new UpdateService();
            UpdateCoachViewModel model = new UpdateCoachViewModel();
            if (LoginState)
            {
                string City = string.Empty;
                string Area = string.Empty;
                string Address = string.Empty;
                User user = GetMemberInfo();
                string MemberId = user.MemberId;
                UpdateCoach updateModel = service.GetCoachInfo(MemberId);
                string[] AddressArray = updateModel.User.Address.Split('|');
                if (AddressArray.Length == 3)
                {
                    City = AddressArray[0];
                    Area = AddressArray[1];
                    updateModel.User.Address = AddressArray[2];
                }

                model.addressCityList = new SelectList(service.GetCity(City), "Value", "Text");
                model.addresAreaList = new SelectList(service.GetArea(), "Value", "Text", Area);
                model.AreaList = new MultiSelectList(service.GetCity(updateModel.OriginArea.ToArray()), "Value", "Text", updateModel.OriginArea);
                model.CourseList = new MultiSelectList(service.GetCourse(updateModel.OriginCourse.ToArray()), "Value", "Text", updateModel.OriginCourse);
                model.TrainingProgramList= new MultiSelectList(service.GetTrainingProgram(updateModel.OriginTrainProgram.ToArray()), "Value", "Text", updateModel.OriginTrainProgram);
                
                model.UpdateModel = updateModel;
                

            }
            else
                return RedirectToAction("Login", "Home");
            
            return View(model);
        }
        [HttpPost]
        public ActionResult UpdateCoach(UpdateCoachViewModel model)
        {
            UpdateService service = new UpdateService();
            Result result = service.UpdateCoach(model);

            return Json(new { success = result.ReturnNo == 1 ? true : false, ex = result.Message },
                  JsonRequestBehavior.AllowGet);
        }

    }
}