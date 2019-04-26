using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class ProductController : ApiController
    {
        List<Product> products = new List<Product> {
            new Product() { Code = 1, Name= "Nokia Lumia", Category="Celulares", Value= 699.99M },
            new Product() { Code = 2, Name= "Samsung Galaxy", Category="Celulares", Value= 1199.99M},
            new Product() { Code = 3, Name= "Teclado", Category="Informática", Value= 130.99M},
            new Product() { Code = 4, Name= "Mouse", Category="Informática", Value= 59.99M},
            new Product() { Code = 5, Name= "Case GoPro", Category="Acessórios", Value= 65.99M},
        };

        //GET
        public IEnumerable<Product> GetAll()
        {
            return products;
        }

        public IHttpActionResult GetByCode(int code)
        {
            var product = products.FirstOrDefault(p => p.Code == code);
            if (product == null) return NotFound();
            return Json<Product>(product);
        }

        public IEnumerable<Product> GetByCategory(string category)
        {
            return products.Where(p => string.Equals(p.Category, category, System.StringComparison.OrdinalIgnoreCase));
        }

        //POST        
        public void Post([FromBody]Product product)
        {
            products.Add(new Product { Code = product.Code, Name = product.Name, Category = product.Category, Value = product.Value });
        }

        //PUT
        public void Put([FromBody]Product product)
        {
            List<Product> prod = products.Where(p => p.Code == product.Code).ToList();

            if(prod.Count > 0)
            {
                products.Remove(prod[0]);
                products.Add(new Product { Code = product.Code, Name = product.Name, Category = product.Category, Value = product.Value });
            }                
            else
                products.Add(new Product { Code = product.Code, Name = product.Name, Category = product.Category, Value = product.Value });
        }

        //DELETE
        public void Delete(int code)
        {
            List<Product> prod = products.Where(p => p.Code == code).ToList();

            if (prod.Count > 0)
                products.Remove(prod[0]);
        }
    }
}
