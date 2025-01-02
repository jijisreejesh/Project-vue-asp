using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Npgsql;
using System;
using Dapper;

namespace dotnet_practice.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        // GET api/products
        // [HttpGet]
        // [Route("productList")]
        // public IEnumerable<Product> Get()
        // {
        //     using var connection = DBContext.GetConnection();
        //     var sql = "SELECT * FROM product";
        //     var products = connection.Query<Product>(sql);

        //     return products;
        // }

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
                    quantityInStock = product.QuantityInStock
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

        // [HttpPut]
        // [Route("edit/{product_id}")]
        // public IActionResult EditProduct(int product_id, [FromBody] Product product)
        // {
        //     if (product == null)
        //     {
        //         return BadRequest("Product Data required");
        //     }
        //     try
        //     {
        //         using var connection = DBContext.GetConnection();
        //         var sql = "UPDATE product SET product_name=@Product_Name,category=@Category,description=@Description,price=@Price,quantity_in_stock=@Quantity_In_Stock where product_id=@product_id";
        //         var result = connection.Execute(sql, new
        //         {
        //             product_id = product.Id,
        //             product_name = product.Name,
        //             category = product.Category,
        //             description = product.Description,
        //             price = product.Price,
        //             quantity_in_stock = product.Quantity_In_Stock
        //         });
        //         if (result > 0)
        //         {
        //             return Ok("Product successfully Edited");
        //         }
        //         return StatusCode(500, "Failed to update product");
        //     }
        //     catch (Exception ex)
        //     {
        //         return StatusCode(500, $"Internal server error: {ex.Message}");
        //     }

        // }
        // [HttpDelete]
        // [Route("delete/{product_id}")]
        // public IActionResult DeleteProduct(int product_id){
        //     using var connection=DBContext.GetConnection();
        //     var sql="DELETE FROM Product WHERE product_id=@product_id";
        //     var result=connection.Execute(sql,new {Id=product_id});
        //     if(result>0)
        //     {
        //         return Ok("Product successfully added");
        //     }
        //     return NotFound("Product not found");
        // }
    }
}


