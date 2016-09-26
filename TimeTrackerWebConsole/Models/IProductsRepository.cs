using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace products.Models
{
    public interface IProductsRepository 
    {
        IEnumerable<Product> GetProducts();
        Product GetProductByID(int id);
        void InsertProduct(Product product);
        void DeleteProduct(int id);
        void EditProduct(Product product);
    }
}