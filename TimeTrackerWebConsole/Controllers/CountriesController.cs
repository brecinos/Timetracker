using countries.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace countries.Controllers
{
    public class CountriesController : Controller
    {
        private ICountriesRepository _repository;
        //
        // GET: /Countries/
        public CountriesController() : this(new CountriesRepository())
        {
        }

    public CountriesController(ICountriesRepository repository)
    {
        _repository = repository;
    }

        public ActionResult Index()
        {
            return View(_repository.GetCountries());
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Country country)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _repository.InsertCountry(country);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    //error msg for failed insert in XML file
                    ModelState.AddModelError("", "Error creating record. " + ex.Message);
                }
            }

            return View(country);
        }
        public ActionResult Edit(int id)
        {
            Country country = _repository.GetCountryByID(id);
            if (country == null)
                return RedirectToAction("Index");
            return View(country);
        }
        [HttpPost]
        public ActionResult Edit(Country country)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _repository.EditCountry(country);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    //error msg for failed edit in XML file
                    ModelState.AddModelError("", "Error editing record. " + ex.Message);
                }
            }

            return View(country);
        }
        public ActionResult Delete(int id)
        {
            Country country = _repository.GetCountryByID(id);
            if (country == null)
                return RedirectToAction("Index");
            return View(country);
        }
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                _repository.DeleteCountry(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                //error msg for failed delete in XML file
                ViewBag.ErrorMsg = "Error deleting record. " + ex.Message;
                return View(_repository.GetCountryByID(id));
            }
        }
    }
}
