using LabDS.App_Start;
using LabDS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LabDS.Controllers
{
    public class AdminController : Controller
    {
        public ActionResult Profile()
        {
            var username = Session["username"].ToString();
            var person = Person.GetByUsername(username);
            return View(person);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            if (Utils.IsAdmin(Session))
            {
                var person = Person.GetById(id);
                return View(person);
            }
            else
                return RedirectToAction("Login", "Person");
        }

        [HttpPost]
        public ActionResult Edit(int id, Person model)
        {
            try
            {
                if (Utils.IsAdmin(Session))
                {
                    if (Person.Edit(model))
                    {
                        return RedirectToAction("Profile");
                    }
                    else
                        return View(model);
                }
                return RedirectToAction("Login", "Person");
            }
            catch
            {
                return View(model);
            }
        }

        [HttpGet]
        public ActionResult UserDetails(int id)
        {
            if (Utils.IsAdmin(Session))
            {
                var person = Person.GetById(id);
                return View(person);
            }
            else
                return RedirectToAction("Login", "Person");
        }

        public ActionResult ListUsers()
        {
            var users = Person.ListUsers();
            return View(users);
        }

        [HttpGet]
        public ActionResult AddUser()
        {
            if (Utils.IsAdmin(Session))
                return View(new Person());
            else
                return RedirectToAction("Login", "Person");
        }

        [HttpPost]
        public ActionResult AddUser(Person person)
        {
            try
            {
                var dbPerson = Person.GetByUsername(person.Username);
                if (dbPerson != null)
                {
                    ModelState.AddModelError("Username", "Username-i eshte i zene, vendosni nje username tjeter.");
                    return View(person);
                }
                if (Person.Register(person))
                    return RedirectToAction("ListUsers");
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
        public ActionResult EditUser(int id)
        {
            if (Utils.IsAdmin(Session))
            {
                var person = Person.GetById(id);
                return View(person);
            }
            else
                return RedirectToAction("Login", "Person");
        }

        [HttpPost]
        public ActionResult EditUser(int id, Person model)
        {
            try
            {
                if (Utils.IsAdmin(Session))
                {
                    if (Person.Edit(model))
                    {
                        return RedirectToAction("ListUsers");
                    }
                    else
                        return View(model);
                }
                return RedirectToAction("Login", "Person");
            }
            catch
            {
                return View(model);
            }
        }

        [HttpGet]
        public ActionResult DeleteUser(int id)
        {
            if (Utils.IsAdmin(Session))
            {
                var person = Person.GetById(id);
                return View(person);
            }
            else
                return RedirectToAction("Login", "Person");
        }

        [HttpPost]
        public ActionResult Delete(int id, Person person)
        {
            try
            {
                if (Utils.IsAdmin(Session))
                {
                    if (Person.Delete(person))
                    {
                        return RedirectToAction("ListUsers");
                    }
                    else
                        return View(person);
                }
                return RedirectToAction("Login", "Person");
            }
            catch
            {
                return View(person);
            }
        }
    }
}
