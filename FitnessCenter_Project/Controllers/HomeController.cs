using FitnessCenter_Project.Service;
using FitnessCenterModel;
using FitnessCenterModel.DTO;
using FitnessCenterModel.Para;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace FitnessCenter_Project.Controllers
{
    public class HomeController : baseController
    {
        //首頁
        public ActionResult Index()
        {
            return View();
        }
        #region 搜尋
        public ActionResult Search()
        {
            return View();
        }
       
       
        #endregion

        #region 登入
        public ActionResult Login()
        {

            //自動登入做在這
            if (HttpContext.User.Identity.IsAuthenticated) //判斷是否已經登入
                return Redirect(FormsAuthentication.DefaultUrl);

            return View();
        }
        [HttpPost]
        public ActionResult LoginUser(LoginModel model)
        {
            LoginService service = new LoginService();
            LoginResult<User> result = service.Login(model);
            DateTime expireDate = model.autoLogin ? DateTime.Now.AddMonths(1) : DateTime.Now.AddMinutes(60);
            //登入成功
            if (result.ReturnNo == 1)
            {
                //登入ticket

                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1,
                    result.Memberinfo.Email,
                    DateTime.Now,
                    expireDate,
                    false,//是否將 Cookie 設定成 Session Cookie，如果是則會在瀏覽器關閉後移除
                    JsonConvert.SerializeObject(result.Memberinfo));

                var encTicket = FormsAuthentication.Encrypt(ticket);

                //FormsAuthentication.RedirectFromLoginPage(result.Memberinfo.Email, bPersistentCookie);
                //若設定為 true ，則 Cookie 資訊會儲存在 Client 端的磁碟中，有效期限預設為 30 分鐘；
                // 若設定為 false ，則 Cookie 資訊只會存放在 Client 端瀏灠器的記憶體中，所以則當使用者關閉瀏灠器後， Cookie 就會失效
                FormsAuthentication.SetAuthCookie(result.Memberinfo.Email, true);
                Response.Cookies.Add(
                       new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));

            }

            return Json(new { success = result.ReturnNo == 1 ? true : false, ex = result.Message },
                JsonRequestBehavior.AllowGet);

        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            TempData.Clear();
            return RedirectToAction("Login", "Home");
        }
        #endregion
        public ActionResult Test()
        { //測試google map
            return View();

        }
        public ActionResult Test2()
        {//好幾個marker
            return View();

        }
        public ActionResult Test3()
        {//json 加載數據到amp
            return View();
        }
    }
}