using persons.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace persons.Controllers
{
    public class PersonsController : Controller
    {
        private IPersonsRepository _repository;  // private ITypesRepository _repository;
        

        //
        // GET: /Persons/
        public PersonsController() : this(new PersonsRepository())
        {
        }



        public PersonsController(IPersonsRepository repository)
    {
        _repository = repository;
    }

        public ActionResult Index()
        {
            return View(_repository.GetPersons());
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Person person)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _repository.InsertPerson(person); 
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    //error msg for failed insert in XML file
                    ModelState.AddModelError("", "Error creating record. " + ex.Message);
                }
            }

            return View(person);
        }
        public ActionResult Edit(int id)
        {
            Person person =  _repository.GetPersonByID(id);  
            if (person == null)
                return RedirectToAction("Index");
            return View(person);
        }

        [HttpPost]
        public ActionResult Edit(Person person)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _repository.EditPerson(person); 
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    //error msg for failed edit in XML file
                    ModelState.AddModelError("", "Error editing record. " + ex.Message);
                }
            }

            return View(person);
        }
        public ActionResult Delete(int id)
        {
            
            Person person = _repository.GetPersonByID(id);
            if (person == null)
                return RedirectToAction("Index");
            return View(person);
        }
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                _repository.DeletePerson(id); 
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                //error msg for failed delete in XML file
                ViewBag.ErrorMsg = "Error deleting record. " + ex.Message;
                
                return View(_repository.GetPersonByID(id)); 
            }
        }
    }
}
