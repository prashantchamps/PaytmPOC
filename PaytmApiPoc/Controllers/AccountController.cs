using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
//using Microsoft.AspNet.Identity;
//using Microsoft.AspNet.Identity.Owin;
//using Microsoft.Owin.Security;
using PaytmApiPoc.Models;

namespace PaytmApiPoc.Controllers
{
    public class AccountController : Controller
    {
        public AccountController()
        {
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel loginData)
        {
            if (loginData.Username.ToLower() == "admin1" && loginData.Password == "admin1")
            {
                return RedirectToAction("Admin1", "Home");
            }
            else if (loginData.Username.ToLower() == "admin2" && loginData.Password == "admin2")
            {
                return RedirectToAction("Admin2", "Home");
            }
            else if (loginData.Username.ToLower() == "parent1" && loginData.Password == "parent1")
            {
                return RedirectToAction("Parent1", "Home");
            }
            else
            {
                return View();
            }
        }
    }
}