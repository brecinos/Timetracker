using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mvc3Razor.Models;

namespace Mvc3Razor.Controllers {
    public class TaskController : Controller 
    {

        // The __usrs class is replacement for a real data access strategy.
        //private static Users _usrs = new Users();
        private static Tasks _task = new Tasks();

        public ActionResult Index() 
        {           
            return View(_task.GetAllTask());

        }

        public ViewResult Create()
        {
            return View(new TaskModel());
        }

        [HttpPost]
        public ViewResult Create(TaskModel um)
        {

            if (!TryUpdateModel(um))
            {
                ViewBag.updateError = "Create Failure";
                return View(um);
            }

            // ToDo: add persistent to DB.
            _task.Add(um);
            return View("Details", um);
        }

        public ActionResult Edit(string id)
        {
            return View(_task.GetTaskByID(id));
        }

        [HttpPost]
        public ActionResult Edit(string id, TaskModel UM)
        {
            if (ModelState.IsValid)
            {
                _task.Update(id, UM);
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Edit");
            }
        }


        public ViewResult Details(string id)
        {
            return View(_task.GetTaskByID(id));
        }


        public ViewResult Delete(string id)
        {
            return View(_task.GetTaskByID(id));
        }

        [HttpPost]
        public RedirectToRouteResult Delete(string id, FormCollection collection)
        {
            _task.Remove(id);
            return RedirectToAction("Index");
        }


      
    }
}


        