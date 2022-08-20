using LabDS.Models;
using LabDS.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LabDS.Controllers
{
    public class PersonController : Controller
    {
        
        [HttpGet]
        public ActionResult Register()
        {
            return View(new Person());
        }

        [HttpPost]
        public ActionResult Register(Person person)
        {
            try
            {
                var dbPerson = Person.GetByUsername(person.Username);
                if(dbPerson != null)
                {
                    ModelState.AddModelError("Username", "Username-i eshte i zene, vendosni nje username tjeter.");
                    return View(person);
                }
                if(Person.Register(person))
                    return RedirectToAction("Login");
                else
                {
                    ModelState.AddModelError("Username", "Regjistrimi nuk u krye dot.");
                    return View(person);
                }
            }
            catch
            {
                ModelState.AddModelError("Username", "Regjistrimi nuk u krye dot.");
                return View(person);
            }
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View(new LoginRequestModel());
        }

        [HttpPost]
        public ActionResult Login(LoginRequestModel model)
        {
            var person = Person.Login(model);
            if(person != null)
            {
                Session["username"] = person.Username;
                Session["role"] = person.Role;
                if (person.Role == RoleType.Admin)
                    return RedirectToAction("Profile", "Admin");
                else if (person.Role == RoleType.User)
                    return RedirectToAction("Profile", "User");
                else
                {
                    return View(model);
                }
            }
            else
                return HttpNotFound();
        }

        [HttpGet]
        public ActionResult Logout()
        {
            Session.RemoveAll();
            Session.Abandon();
            return RedirectToAction("Login");
        }
      
    }
}
