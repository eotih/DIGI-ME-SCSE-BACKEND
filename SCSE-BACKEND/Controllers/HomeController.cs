using SCSE_BACKEND.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SCSE_BACKEND.Controllers
{

    public class HomeController : Controller
    {
        
        private static string username = "SCSEAPI";
        private static string password = "SCSEPASSWORD";

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginAPI api)
        {
            if (username.Equals(api.Username) && password.Equals(api.Password))
            {
                return Redirect("~/Help");
            }
            return RedirectToAction("Login");
        }
    }
}
