using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Npgsql;
using System;
using Dapper;
using backend.Models;
namespace backend.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        // GET api/products
        [HttpGet]
        [Route("productList")]
        public IEnumerable<Product> Get()
        {
            try{
                using var connection = DBContext.GetConnection();
            var sql = "SELECT * FROM Product ORDER BY id";
            var products = connection.Query<Product>(sql);
            Console.WriteLine(products);
            return products;
            }
            catch(Exception er){
                 Console.WriteLine ($"error :{er} ");
                 return Enumerable.Empty<Product>();
            }
        }

        // //GET single product
        // [HttpGet]
        // [Route("list/{product_id}")]
        // public IActionResult GetById(int product_id){
        //     using var connection=DBContext.GetConnection();
        //     var sql="SELECT * FROM product where product_id=@product_id";
          
        //     var result=connection.QuerySingleOrDefault<Product>(sql,new{Product_Id=product_id});
        //     if(result==null)
        //      return NotFound("Product not found");
        //     return Ok(result);
           
        // }


        [HttpGet]
        [Route("getProductList")]
        public IEnumerable<AutocompleteModel>GetProductList()
        {
            try{
            using var connection=DBContext.GetConnection();
            var sql=$"SELECT id, name as Label FROM Product ORDER BY id";
            var productDetails=connection.Query<AutocompleteModel>(sql);
            return productDetails;
            }
            catch(Exception er){
                 Console.WriteLine ($"error :{er} ");
                 return Enumerable.Empty<AutocompleteModel>();
            }
        }


        //insert api/products
        [HttpPost]
        [Route("AddProduct")]
        public IActionResult AddProduct([FromBody] Product product)
        {

            if (product == null)
            {
                return BadRequest("Product data is required");
            }
            try
            {
                using var connection = DBContext.GetConnection();
                var sql = "INSERT INTO Product (name,category,price,quantity_in_stock,description)" +
                "VALUES(@name,@category,@price,@quantityInStock,@description)";
                var result = connection.Execute(sql, new
                {
                   
                    name = product.Name,
                    category = product.Category,
                    description = product.Description,
                    price = product.Price,
                    quantityInStock = product.Quantity_In_Stock
                });
                if (result > 0)
                {
                    return Ok("Product successfully added");
                }
                return StatusCode(500, "Failed to add product");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut]
        [Route("edit")]
        public IActionResult EditProduct([FromBody] Product product)
        {
            if (product == null)
            {
                return BadRequest("Product Data required");
            }
            try
            {
                using var connection = DBContext.GetConnection();
                var sql = "UPDATE Product SET name=@name,category=@category,description=@description,price=@price,quantity_in_stock=@quantityInStock  where id=@id";
                var result = connection.Execute(sql, new
                {
                    id = product.Id,
                    name = product.Name,
                    category = product.Category,
                    description = product.Description,
                    price = product.Price,
                    quantityInStock = product.Quantity_In_Stock
                });
                if (result > 0)
                {
                    return Ok("Product successfully Edited");
                }
                return StatusCode(500, "Failed to update product");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }

        [HttpDelete]
        [Route("delete/{product_id}")]
        public IActionResult DeleteProduct(int product_id){
            using var connection=DBContext.GetConnection();
            var sql="DELETE FROM Product WHERE id=@id";
            var result=connection.Execute(sql,new {id=product_id});
            if(result>0)
            {
                return Ok("Product successfully Deleted");
            }
            return NotFound("Product not found");
        }
    }
}


