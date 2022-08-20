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
            analysis.Parameters = Parameter.ListParameters(id);
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
                        return RedirectToAction("AddParameter");
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
        public ActionResult ListParameters(int id)
        {
            var parameters = Parameter.ListParameters(id);
            return View(parameters);
        }

        [HttpGet]
        public ActionResult ParameterDetails(int id)
        {
            var parameter = Parameter.GetById(id);
            return View(parameter);
        }

        [HttpGet]
        public ActionResult ModifyListParameters(int id)
        {
            if (Utils.IsAdmin(Session))
            {
                return View(new Parameter()
                {
                    AnalysisId = id
                });
            }
            else
                return RedirectToAction("Login", "Person");
        }

        [HttpPost]
        public ActionResult ModifyListParameters(Parameter model, int id)
        {
            try
            {
                if (Utils.IsAdmin(Session))
                {
                    
                    model.AnalysisId = id;
                    if (Parameter.AddParameter(model))
                        return RedirectToAction("ListParameters", new {id = id});
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
        public ActionResult AddParameter()
        {
            if (Utils.IsAdmin(Session))
            {
                var analysis = Analysis.GetLast();
                var id = analysis.Id;
                return View(new Parameter()
                {
                    AnalysisId=id,
                });
                
            }
            else
                return RedirectToAction("Login", "Person");
        }

        [HttpPost]
        public ActionResult AddParameter(Parameter model)
        {
            try
            {
                if (Utils.IsAdmin(Session))
                {
                    var analysis = Analysis.GetLast();
                    model.AnalysisId = analysis.Id;
                    if (Parameter.AddParameter(model))
                        return RedirectToAction("AddParameter");
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
        public ActionResult EditParameter(int id)
        {
            if (Utils.IsAdmin(Session))
            {
                var parameter = Parameter.GetById(id);
                return View(parameter);
            }
            else
                return RedirectToAction("Login", "Person");

        }

        [HttpPost]
        public ActionResult EditParameter(int id, Parameter model)
        {
            try
            {
                if (Utils.IsAdmin(Session))
                {
                    var analysisId = Parameter.GetById(id).AnalysisId;
                    model.AnalysisId = analysisId;
                    if (Parameter.Edit(model))
                    {
                        return RedirectToAction("ListParameters", new { id = model.AnalysisId });
                    }
                    else
                    {
                        ModelState.AddModelError("Name", "Modifikimi s'u krye dot.");
                        return View(model);
                    }
                        
                }
                return RedirectToAction("Login", "Person");
            }
            catch
            {
                ModelState.AddModelError("Name", "Modifikimi nuk u krye dot.");
                return View(model);
            }
        }

        [HttpGet]
        public ActionResult DeleteParameter(int id)
        {
            if (Utils.IsAdmin(Session))
            {
                var parameter = Parameter.GetById(id);
                return View(parameter);
            }
            else
                return RedirectToAction("Login", "Person");
        }


        [HttpPost]
        public ActionResult DeleteParameter(int id, Parameter parameter)
        {
            try
            {
                var analysisId = Parameter.GetById(id).AnalysisId;
                if (Utils.IsAdmin(Session))
                {
                    if (Parameter.DeleteParameter(parameter))
                    {
                        return RedirectToAction("ListParameters", new {id = analysisId});
                    }
                    else
                        return View(parameter);
                }
                return RedirectToAction("Login", "Person");
            }
            catch
            {
                return View(parameter);
            }
        }
    }
}
