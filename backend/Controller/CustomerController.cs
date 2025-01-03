using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Npgsql;
using System;
using Dapper;
using System.Security.AccessControl;
using backend.Models;

namespace dotnet_practice.Controller
{
      [ApiController]
    [Route("api/[controller]")]
    public class CustomerController:ControllerBase
    {

        [HttpGet]
        [Route("getdata")]
        public IEnumerable<Customer>Get()
        {
            try{
            using var connection=DBContext.GetConnection();
            var sql="SELECT * FROM Customer ORDER BY id";
            var customerDetails=connection.Query<Customer>(sql);
            return customerDetails;
            }
            catch(Exception er){
                 Console.WriteLine ($"error :{er} ");
                 return Enumerable.Empty<Customer>();
            }
        }

        [HttpGet]
        [Route("getCustomerList")]
        public IEnumerable<AutocompleteModel>GetCustomerList()
        {
            try{
            using var connection=DBContext.GetConnection();
            var sql=$"SELECT id, name as Label FROM Customer ORDER BY id";
            var customerDetails=connection.Query<AutocompleteModel>(sql);
            return customerDetails;
            }
            catch(Exception er){
                 Console.WriteLine ($"error :{er} ");
                 return Enumerable.Empty<AutocompleteModel>();
            }
        }


        [HttpPost]
        [Route("AddCustomer")]
        public IActionResult AddCustomer([FromBody]Customer customer)
        {
            if(customer==null)
            {
                return BadRequest("CustomerData Required");
            }
            try{
                using var connection=DBContext.GetConnection();
                var sql="INSERT INTO Customer(name,phone,email,city)values(@Name,@Phone,@Email,@City)";
                var result=connection.Execute(sql,new{
                    name=customer.Name,
                    phone=customer.Phone,
                    email=customer.Email,
                    city=customer.City
                });
                if(result>0)
                {
                    return Ok("Customer added");
                }
                return StatusCode(500,"Failed to add product");
            }
            catch(Exception ex)
            {
                return StatusCode(500,$"Internal Server error : {ex.Message}");
            }
        }

        [HttpPut]
        [Route("Edit")]
        public IActionResult UpdateCustomer([FromBody]Customer customer)
        {
             if (customer == null)
            {
                return BadRequest("Customer details required");
            }
            try{
            using var connection=DBContext.GetConnection();
            var sql="UPDATE Customer SET name=@name,phone=@phone,email=@email,city=@city WHERE id=@id";
            var result=connection.Execute(sql,new{
                    id=customer.Id,
                    name=customer.Name,
                    phone=customer.Phone,
                    email=customer.Email,
                    city=customer.City
                });
                if(result>0)
                {
                    return Ok("Customer details updated");
                }
                return StatusCode(500,"Failed to add product");
            }
            catch(Exception ex)
            {
                return StatusCode(500,$"Internal Server error : {ex.Message}");
            }
    }


        [HttpDelete]
        [Route("Delete/{customer_id}")]
        public IActionResult DeleteCustomer(int customer_id){
           using var connection=DBContext.GetConnection();
           var sql="DELETE FROM customer where id=@id";
           var result=connection.Execute(sql,new {Id=customer_id});
           if(result>0)
           {
            return Ok("customer successfully deleted");
           }
           return NotFound("Customer not found");
        }

    }
}