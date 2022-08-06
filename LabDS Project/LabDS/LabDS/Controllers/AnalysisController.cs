using LabDS.App_Start;
using LabDS.Models;
using LabDS.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LabDS.Controllers
{
    public class AnalysisController : Controller
    {
        // GET: Analysis
        public ActionResult List()
        {
            var analyzes = Analysis.ListAnalyzes();
            return View(analyzes);
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var analysis = Analysis.GetById(id);
            return View(analysis);
        }

        [HttpGet]
        public ActionResult Insert()
        {
            if (Utils.IsAdmin(Session))
            {
                var categories = AnalysisCategory.ListCategories();
                ViewData["Categories"] = categories;
                return View(new AnalysisAddRequest());
            }
            else
            {
                return RedirectToAction("Login", "Person");
            }
        }

        [HttpPost]
        public ActionResult Insert(AnalysisAddRequest model)
        {
            try
            {
                if (Utils.IsAdmin(Session))
                {
                    var dbProduct = Analysis.GetByName(model.Name);
                    if (dbProduct != null)
                    {
                        ModelState.AddModelError("Name", "Ekziston nje tjeter analize me kete emer");
                        return View(model);
                    }
                    if (Analysis.Insert(model))
                        return RedirectToAction("List");
                    return View(model);
                }
                else
                {
                    return RedirectToAction("Login", "Person");
                }
            }
            catch
            {
                return View(model);
            }
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            if (Utils.IsAdmin(Session))
            {
                var categories = AnalysisCategory.ListCategories();
                ViewData["Categories"] = categories;
                var analysis = Analysis.GetById(id);
                return View(analysis);
            }
            else
                return RedirectToAction("Login", "Person");
            
        }

        
        [HttpPost]
        public ActionResult Edit(int id, Analysis model)
        {
            try
            {
                if (Utils.IsAdmin(Session))
                {
                    if (Analysis.Edit(model))
                    {
                        return RedirectToAction("List");
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
        public ActionResult Delete(int id)
        {
            if (Utils.IsAdmin(Session))
            {
                var analysis = Analysis.GetById(id);
                return View(analysis);
            }
            else
                return RedirectToAction("Login", "Person");
        }

        
        [HttpPost]
        public ActionResult Delete(int id, Analysis model)
        {
            try
            {
                if (Utils.IsAdmin(Session))
                {
                    if (Analysis.Delete(model))
                    {
                        return RedirectToAction("List");
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
    }
}
