using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace products.Models
{
    public class  ProductsRepository : IProductsRepository   
    {
        private List<Product> allProducts;
        private XDocument productsData;
        //constructor
        public ProductsRepository()
        {
            allProducts = new List<Product>();
            productsData = XDocument.Load(HttpContext.Current.Server.MapPath("~/App_Data/Products.xml"));


            var products = from product in productsData.Descendants("product")
                        select new Product((int)product.Element("id"), product.Element("name").Value,
                            product.Element("description").Value );
            allProducts.AddRange(products.ToList<Product>());
            
        }
        // return a list of all countries
        public IEnumerable<Product> GetProducts()
        {
            return allProducts;
        }
        public Product GetProductByID(int id)
        {
            return allProducts.Find(products => products.id == id);
        }
        //insert record
        public void InsertProduct(Product product)
        {
            product.id = (int)(from b in productsData.Descendants("product") orderby (int)b.Element("id") descending select (int)b.Element("id")).FirstOrDefault() + 1;

            productsData.Root.Add(new XElement("product", new XElement("id", product.id), new XElement("name", product.name),
                                 new XElement("description", product.description)  )   );

            productsData.Save(HttpContext.Current.Server.MapPath("~/App_Data/Products.xml"));
        }
        public void DeleteProduct(int id)
        {
            productsData.Root.Elements("product").Where(i => (int)i.Element("id") == id).Remove();
            productsData.Save(HttpContext.Current.Server.MapPath("~/App_Data/Products.xml"));
        }
        public void EditProduct(Product product)
        {
            XElement node = productsData.Root.Elements("product").Where(i => (int)i.Element("id") == product.id).FirstOrDefault();

            node.SetElementValue("id", product.id);
            node.SetElementValue("name", product.name);
            node.SetElementValue("description", product.description);
            productsData.Save(HttpContext.Current.Server.MapPath("~/App_Data/Products.xml"));
        }

    }
}