using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mvc3Razor.Models;

namespace Mvc3Razor.Controllers {
    public class AssignController : Controller 
    {

        // The __usrs class is replacement for a real data access strategy.
        //private static Users _usrs = new Users();
        private static Assign _assign = new Assign();

        public ActionResult Index() 
        {
            return View(_assign.GetAllTask());

        }

        public ViewResult Create()
        {
            return View(new AssignModel());
        }

        [HttpPost]
        public ViewResult Create(AssignModel um)
        {

            if (!TryUpdateModel(um))
            {
                ViewBag.updateError = "Create Failure";
                return View(um);
            }

            // ToDo: add persistent to DB.
            _assign.Add(um);
            return View("Details", um);
        }

        public ActionResult Edit(string id)
        {
            return View(_assign.GetTaskByID(id));
        }

        [HttpPost]
        public ActionResult Edit(string id, AssignModel UM)
        {
            if (ModelState.IsValid)
            {
                _assign.Update(id, UM);
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Edit");
            }
        }


        public ViewResult Details(string id)
        {
            return View(_assign.GetTaskByID(id));
        }


        public ViewResult Delete(string id)
        {
            return View(_assign.GetTaskByID(id));
        }

        [HttpPost]
        public RedirectToRouteResult Delete(string id, FormCollection collection)
        {
            _assign.Remove(id);
            return RedirectToAction("Index");
        }


      
    }
}


        