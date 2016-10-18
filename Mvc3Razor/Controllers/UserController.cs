using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mvc3Razor.Models;

namespace Mvc3Razor.Controllers {
    public class UserController : Controller 
    {

        // The __usrs class is replacement for a real data access strategy.
        private static Users _usrs = new Users();

        public ActionResult Index() 
        {           
            return View(_usrs.GetAllUsers());
        }

        public ViewResult Details(string id) 
        {
            return View(_usrs.GetUserByID(id));
        }

        public ActionResult Edit(string id) 
        {
            return View(_usrs.GetUserByID(id));
        }

        [HttpPost]
        public ActionResult Edit(string id, UserModel UM)
        {
            if (ModelState.IsValid)
            {
                _usrs.Update(id, UM);
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Edit");
            }
        }     
        

        public ActionResult About() {
            return View();
        }

        public ViewResult Create() {
            return View(new UserModel());
        }

        [HttpPost]
        public ViewResult Create(UserModel um) 
        {

            if (!TryUpdateModel(um)) {
                ViewBag.updateError = "Create Failure";
                return View(um);
            }

            // ToDo: add persistent to DB.
            _usrs.Add(um);
            return View("Details", um);
        }

        public ViewResult Delete(string id) 
        {
            return View(_usrs.GetUserByID(id));
        }

        [HttpPost]
        public RedirectToRouteResult Delete(string id, FormCollection collection) 
        {
            _usrs.Remove(id);
            return RedirectToAction("Index");
        }


    }
}
