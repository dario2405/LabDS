using LabDS.App_Start;
using LabDS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LabDS.Controllers
{
    public class UserController : Controller
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
            if (Utils.IsUser(Session))
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
                if (Utils.IsUser(Session))
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

        // GET: User
        public ActionResult ListPacients()
        {
            var pacients = Pacient.ListPacients();
            return View(pacients);
        }

        [HttpGet]
        public ActionResult AddPacient()
        {
            if (Utils.IsUser(Session))
            {
                return View(new Pacient());
            }
            else
                return RedirectToAction("Login", "Person");
        }

        [HttpPost]
        public ActionResult AddPacient(Pacient pacient)
        {
            try
            {
                var dbPacient = Pacient.GetByFullName(pacient.Name, pacient.Surname);
                if (dbPacient != null)
                {
                    ModelState.AddModelError("Name", "Pacienti eshte i regjistruar.");
                    return View(pacient);
                }
                if (Pacient.AddPacient(pacient))
                {
                    return RedirectToAction("Insert");
                }
                   
                else
                {
                    ModelState.AddModelError("Name", "Regjistrimi nuk u krye dot.");
                    return View(pacient);
                }
            }
            catch
            {
                ModelState.AddModelError("Name", "Regjistrimi nuk u krye dot.");
                return View(pacient);
            }
        }

        [HttpGet]
        public ActionResult EditPacient(int id)
        {
            if (Utils.IsUser(Session))
            {
                var pacient = Pacient.GetById(id);
                return View(pacient);
            }
            else
                return RedirectToAction("Login", "Person");
        }

        [HttpPost]
        public ActionResult EditPacient(int id, Pacient model)
        {
            try
            {
                if (Utils.IsUser(Session))
                {
                    if (Pacient.EditPacient(model))
                    {
                        return RedirectToAction("ListPacients");
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
        public ActionResult DeletePacient(int id)
        {
            if (Utils.IsUser(Session))
            {
                var pacient = Pacient.GetById(id);
                return View(pacient);
            }
            else
                return RedirectToAction("Login", "Person");
        }

        [HttpPost]
        public ActionResult DeletePacient(int id, Pacient model)
        {
            try
            {
                if (Utils.IsUser(Session))
                {
                    if (Pacient.DeletePacient(model))
                    {
                        return RedirectToAction("ListPacients");
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
        public ActionResult ListAnalyzes(int id)
        {
            if (Utils.IsUser(Session))
            {
                var analyzes = PacientAnalysis.ListAnalyzes(id);
                return View(analyzes);
            }
            else
                return RedirectToAction("Login", "Person");
        }
        [HttpGet]
        public ActionResult Insert()
        {
            var analyzes = Analysis.ListAnalyzes();
            ViewData["Analyzes"] = analyzes;
            return View(new PacientAnalysis());
        }

        [HttpPost]
        public ActionResult Insert(PacientAnalysis model)
        {
            try
            {
                if (Utils.IsUser(Session))
                {
                    var analysis = Analysis.GetById(model.AnalysisId);
                    var pacient = Pacient.GetLast();
                    model.PacientId = pacient.Id;
                    model.Name = analysis.Name;
                    model.Price = analysis.Price;
                    model.CategoryId = analysis.CategoryId;
                    PacientAnalysis.Insert(model);
                    return RedirectToAction("ListPacients", "User");
                }
                else
                    return RedirectToAction("Login", "Person");
            }
            catch
            {
                return View();
            }
        }
        [HttpGet]
        public ActionResult AddAnalysis(int id)
        {
            var analyzes = Analysis.ListAnalyzes();
            ViewData["Analyzes"] = analyzes;
            return View(new PacientAnalysis()
            {
                PacientId =id,
            });
        }

        [HttpPost]
        public ActionResult AddAnalysis(int id, PacientAnalysis model)
        {
            try
            {
                if (Utils.IsUser(Session))
                {
                    var analysis = Analysis.GetById(model.AnalysisId);
                    var pacient = Pacient.GetLast();
                    model.PacientId = id;
                    model.Name = analysis.Name;
                    model.Price = analysis.Price;
                    model.CategoryId = analysis.CategoryId;
                    PacientAnalysis.Insert(model);
                    return RedirectToAction("ListPacients", "User");
                }
                else
                    return RedirectToAction("Login", "Person");
            }
            catch
            {
                return View();
            }
        }
    }
}
