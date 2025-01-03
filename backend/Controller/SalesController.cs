using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Npgsql;
using System;
using Dapper;
using System.Security.AccessControl;

namespace dotnet_practice.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class SalesController : ControllerBase
    {
        
        [HttpGet]
        [Route("GetSales")]
        public IEnumerable<SalesViewModel> Get()
        {
            using var connection = DBContext.GetConnection();
            var sql = "SELECT * FROM sales ORDER BY id";
            var sales = connection.Query<SalesViewModel>(sql);

            return sales;
        }

       
        [HttpPost]
        [Route("AddSales")]
        public IActionResult AddSales([FromBody] Sales sales)
        {

            if (sales == null)
            {
                return BadRequest("Product data is required");
            }
            try
            {
                using var connection = DBContext.GetConnection();
                var sql = "INSERT INTO Sales (customer_id,product_id,quantity,total_price,sales_date,payment_method,status)" +
                "VALUES(@Customer_Id,@Product_Id,@Quantity,@Total_Price,@Sales_Date,@Payment_Method,@Status)";
                var result = connection.Execute(sql, new
                {
                    customer_id=sales.Customer_Id,
                    product_id = sales.Product_Id,
                    quantity= sales.Quantity,
                    total_price=sales.Total_Price,
                    sales_date=sales.Sales_Date,
                    payment_method=sales.Payment_Method,
                    status=sales.Status
                });
                if (result > 0)
                {
                    return Ok("Sales successfully added");
                }
                return StatusCode(500, "Failed to add sales");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpPut]
        [Route("Edit")]
        public IActionResult EditProduct([FromBody]Sales sales)
        {
            if(sales==null){
                return BadRequest("Sales Data Required");
            }
            try{
                using var connection=DBContext.GetConnection();
                var sql="UPDATE SALES SET customer_id=@Customer_Id,product_id=@Product_Id,quantity=@Quantity,total_price=@Total_Price,sales_date=@Sales_Date,payment_method=@Payment_Method,status=@Status where id=@id";
                var result=connection.Execute(sql,new{
                     id=sales.Id,
                    product_id=sales.Product_Id,
                    customer_id=sales.Customer_Id,
                    quantity=sales.Quantity,
                    total_price=sales.Total_Price,
                    sales_date=sales.Sales_Date,
                    payment_method=sales.Payment_Method,
                    status=sales.Status
                });
                if(result>0)
                {
                    return Ok("sales details updated");
                }
                 return StatusCode(500, "Failed to update product");
            }
            
            catch(Exception ex)
            {
                return StatusCode(500,$"Internal server error : {ex.Message}");
            }
        }


        [HttpDelete]
        [Route("Delete/{sales_id}")]
        public IActionResult DeleteProduct(int sales_id){
            using var connection=DBContext.GetConnection();
            var sql="DELETE FROM Sales WHERE id=@id";
            var result=connection.Execute(sql,new {Id=sales_id});
            if(result>0)
            {
                return Ok("Deleted Sales Details");
            }
            return NotFound("Product not found");
        }
    }
}


