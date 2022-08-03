using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LabDS.Controllers
{
    public class AnalysisCategoryController : Controller
    {
        // GET: AnalysisCategory
        public ActionResult Index()
        {
            return View();
        }

        // GET: AnalysisCategory/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AnalysisCategory/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AnalysisCategory/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: AnalysisCategory/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AnalysisCategory/Edit/5
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

        // GET: AnalysisCategory/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AnalysisCategory/Delete/5
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
