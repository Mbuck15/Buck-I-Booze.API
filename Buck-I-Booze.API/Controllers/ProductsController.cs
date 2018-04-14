using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuckIBooze.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace BuckIBooze.API.Controllers
{
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        private readonly LiquorStoreContext db;
        public ProductsController(LiquorStoreContext db)
        {
            this.db = db;

            if (this.db.Products.Count() == 0)
            {
                this.db.Products.Add(new Product {
                    Id = 1,
                    name = "Tito's",
                    price = 15,
                    size = "bottle",
                    type = "Vodka",
                    imgName = "Titos",
                    imgURL = "https://www.haskells.com/media/catalog/product/cache/1/image/816x1200/040ec09b1e35df139433887a97daa66f/1/0/102060_0_1_1.jpg"
                });

                this.db.Products.Add(new Product 
                {
                    Id = 2,
                    name = "Jack Daniel's",
                    price = 17,
                    size = "bottle",
                    type = "Whiskey",
                    imgName = "Jack",
                    imgURL = "https://drizly-products1.imgix.net/ci-jack-daniels-old-no-7-eb589ada0c4a8027.jpeg?auto=format%2Ccompress&fm=jpeg&q=20"
                });

                this.db.SaveChanges();
            }
        }
        
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(db.Products);
        }

        [HttpGet("{id}", Name="GetProduct")]
        public IActionResult GetById(int id)
        {
            var product = db.Products.Find(id);

            if(product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }
        
        [HttpPost]
        public IActionResult Post([FromBody]Product product)
        {
            if(product == null)
            {
                return BadRequest();
            }

            this.db.Products.Add(product);
            this.db.SaveChanges();

            return CreatedAtRoute("GetProduct", new {id = product.Id}, product);
        }
        
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Product newProduct)
        {
            if (newProduct == null || newProduct.Id != id)
            {
                return BadRequest();
            }
            var currentProduct = this.db.Products.FirstOrDefault(x => x.Id == id);

            if (currentProduct == null)
            {
                return NotFound();
            }


            this.db.Products.Update(currentProduct);
            this.db.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var product = this.db.Products.FirstOrDefault(x => x.Id == id);

            if (product == null)
            {
                return NotFound();
            }

        
            this.db.Products.Remove(product);
            this.db.SaveChanges();

            return NoContent();
        }
    }
    
}
