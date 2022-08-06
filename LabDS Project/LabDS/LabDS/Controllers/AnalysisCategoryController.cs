using LabDS.App_Start;
using LabDS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LabDS.Controllers
{
    public class AnalysisCategoryController : Controller
    {
        public ActionResult List()
        {
            var categories = AnalysisCategory.ListCategories();
            return View(categories);
        }
        public ActionResult Details(int id)
        {
            var category = AnalysisCategory.GetById(id);
            return View(category);
        }

        [HttpGet]
        public ActionResult Insert()
        {
            if (Utils.IsAdmin(Session))
            {
                return View(new AnalysisCategory());
            }
            else
                return RedirectToAction("Login", "Person");
        }

        [HttpPost]
        public ActionResult Insert(AnalysisCategory model)
        {
            try
            {
                if (Utils.IsAdmin(Session))
                {
                    var dbCategory = AnalysisCategory.GetByName(model.Name);
                    if (dbCategory != null)
                    {
                        ModelState.AddModelError("Name", "Ekziston  nje tjeter kategori me kete emer.");
                        return View(model);
                    }
                    else
                    {
                        if (AnalysisCategory.Insert(model))
                        {
                            return RedirectToAction("List");
                        }
                        else
                        {
                            ModelState.AddModelError("Name", "Nuk u shtua dot!");
                            return View(model);
                        }
                    }
                   
                }
                else
                    return RedirectToAction("Login", "Person");
            }
            catch
            {
                return RedirectToAction("List");
            }
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var category = AnalysisCategory.GetById(id);
            return View(category);
        }

        [HttpPost]
        public ActionResult Edit(int id, AnalysisCategory category)
        {
            try
            {
                if (Utils.IsAdmin(Session))
                {
                    if (AnalysisCategory.Edit(category))
                        return RedirectToAction("List");
                    else
                        return View(category);
                }
                else
                    return RedirectToAction("Login", "Person");
            }
            catch
            {
                return View(category);
            }
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var category = AnalysisCategory.GetById(id);
            return View(category);
        }

        
        [HttpPost]
        public ActionResult Delete(int id, AnalysisCategory category)
        {
            try
            {
                if (Utils.IsAdmin(Session))
                {
                    if (AnalysisCategory.Delete(category))
                    {
                        return RedirectToAction("List");
                    }
                    else return View(category);

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
