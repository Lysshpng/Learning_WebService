using MyDemoWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MyDemoWebApi.Controllers
{
    /// <summary>
    /// Product 控制器
    /// </summary>
    public class ProductController : ApiController
    {
        Product[] products = new Product[]
        {
            new Product { Id = 1, Name = "笔记本电脑", Category = "电子产品", Price = 5999.99M },
            new Product { Id = 2, Name = "智能手机", Category = "电子产品", Price = 3999.99M },
            new Product { Id = 3, Name = "平板电脑", Category = "电子产品", Price = 2999.99M },
            new Product { Id = 4, Name = "无线耳机", Category = "电子产品", Price = 999.99M },
            new Product { Id = 5, Name = "智能手表", Category = "电子产品", Price = 1999.99M }
        };

        public IEnumerable<Product> GetAllProducts()
        {
            return products;
        }

        public IHttpActionResult GetProductById(int id)
        {
            var product = products.FirstOrDefault((p) => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
    }
}
