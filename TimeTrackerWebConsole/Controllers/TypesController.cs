using types.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace types.Controllers
{
    public class TypesController : Controller
    {
        private ITypesRepository _repository;
        

        //
        // GET: /Types/
        public TypesController() : this(new TypesRepository())
        {
        }

         

    public TypesController(ITypesRepository repository)
    {
        _repository = repository;
    }

        public ActionResult Index()
        {
            return View(_repository.GetTypes());
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Typex type)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _repository.InsertType(type);
                    //_repository.InsertCountry(country);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    //error msg for failed insert in XML file
                    ModelState.AddModelError("", "Error creating record. " + ex.Message);
                }
            }

            return View(type);
        }
        public ActionResult Edit(int id)
        {
            Typex type =  _repository.GetTypeByID(id);   //_repository.GetCountryByID(id);
            //type type = _repository.GetCountryByID(id);
            if (type == null)
                return RedirectToAction("Index");
            return View(type);
        }
        [HttpPost]
        public ActionResult Edit(Typex type)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //_repository.EditCountry(type);
                    _repository.EditType(type);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    //error msg for failed edit in XML file
                    ModelState.AddModelError("", "Error editing record. " + ex.Message);
                }
            }

            return View(type);
        }
        public ActionResult Delete(int id)
        {
            //Type type = _repository.//GetCountryByID(id);
            Typex type =  _repository.GetTypeByID(id);
            if (type == null)
                return RedirectToAction("Index");
            return View(type);
        }
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                //_repository.DeleteCountry(id);
                _repository.DeleteType(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                //error msg for failed delete in XML file
                ViewBag.ErrorMsg = "Error deleting record. " + ex.Message;
                //return View(_repository.GetCountryByID(id));
                return View(_repository.GetTypeByID(id));
            }
        }
    }
}
