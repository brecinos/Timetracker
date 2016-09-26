using products.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace products.Controllers
{
    public class ProductsController : Controller
    {
        private IProductsRepository _repository;  
        

        //
        // GET: /Products/
        public ProductsController() : this(new ProductsRepository())
        {
        }



        public ProductsController(IProductsRepository repository)
    {
        _repository = repository;
    }

        public ActionResult Index()
        {
            return View(_repository.GetProducts());
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _repository.InsertProduct(product); 
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    //error msg for failed insert in XML file
                    ModelState.AddModelError("", "Error creating record. " + ex.Message);
                }
            }

            return View(product);
        }
        public ActionResult Edit(int id)
        {
            Product product =  _repository.GetProductByID(id);  
            if (product == null)
                return RedirectToAction("Index");
            return View(product);
        }

        [HttpPost]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _repository.EditProduct(product); 
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    //error msg for failed edit in XML file
                    ModelState.AddModelError("", "Error editing record. " + ex.Message);
                }
            }

            return View(product);
        }
        public ActionResult Delete(int id)
        {
            
            Product product = _repository.GetProductByID(id);
            if (product == null)
                return RedirectToAction("Index");
            return View(product);
        }
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                _repository.DeleteProduct(id); 
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                //error msg for failed delete in XML file
                ViewBag.ErrorMsg = "Error deleting record. " + ex.Message;
                
                return View(_repository.GetProductByID(id)); 
            }
        }
    }
}
