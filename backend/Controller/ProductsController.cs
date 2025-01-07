using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Npgsql;
using System;
using Dapper;
using backend.Models;
using backend.Interfaces;
namespace backend.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService productService;

        public ProductsController(IProductService prdtService)
        {
            productService = prdtService;
        }

        // GET api/products
        [HttpGet]
        [Route("productList")]
        public IEnumerable<Product> Get()
        {
            return productService.GetList();
        }

        //GET single product
        [HttpGet]
        [Route("list/{product_id}")]
        public IActionResult GetProductById(int product_id)
        {
            var product = productService.GetById(product_id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);

        }


        [HttpGet]
        [Route("getProductList")]
        public IEnumerable<AutocompleteModel> GetProductList()
        {
            return productService.GetProductList();
        }

        [HttpPost]
        [Route("AddProduct")]
        public IActionResult AddProduct([FromBody] Product product)
        {
            if (product == null)
            {
                return BadRequest("Product data is required");
            }

            if (productService.Add(product))
            {
                return Ok("Saved Successfully!");
            }
            else return StatusCode(500, "Failed to add product");

        }

        [HttpPut]
        [Route("edit")]
        public IActionResult EditProduct([FromBody] Product product)
        {
            if (product == null)
            {
                return BadRequest("Product Data required");
            }
            if (productService.Edit(product))
            {
                return Ok("Edited Successfully!");
            }
            else return StatusCode(500, "Failed to edit product");


        }

        [HttpDelete]
        [Route("delete/{product_id}")]
        public IActionResult DeleteProduct(int product_id)
        {
            if (productService.Delete(product_id))
            {
                return Ok("Deleted Successfully!");
            }
            else return StatusCode(500, "Failed to delete product");

        }
    }
}


