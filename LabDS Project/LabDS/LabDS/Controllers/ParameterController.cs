using LabDS.App_Start;
using LabDS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LabDS.Controllers
{
    public class ParameterController : Controller
    {
        // GET: Parameter
        public ActionResult Index()
        {
            return View();
        }

        // GET: Parameter/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        [HttpGet]
        public ActionResult AddParameter(int id)
        {
            if (Utils.IsAdmin(Session))
            {
                return View(new Parameter()
                {
                    AnalysisId = id,
                });
            }
            else
                return RedirectToAction("Login", "Person");
        }

        // POST: Parameter/Create
        [HttpPost]
        public ActionResult AddParameter(Parameter model)
        {
            try
            {
                if (Utils.IsAdmin(Session))
                {
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

        // GET: Parameter/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Parameter/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Parameter/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Parameter/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
