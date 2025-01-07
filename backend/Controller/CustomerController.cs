using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Npgsql;
using System;
using Dapper;
using System.Security.AccessControl;
using backend.Models;
using backend.Interfaces;

namespace backend.Controller
{
      [ApiController]
    [Route("api/[controller]")]
    public class CustomerController:ControllerBase
    {

        private readonly ICustomerService customerService;
        public CustomerController(ICustomerService custService){
            customerService=custService;
        }

        [HttpGet]
        [Route("getdata")]
        public IEnumerable<Customer> Get()
        {
            return customerService.GetList();
        }

        [HttpGet]
        [Route("getCustomerList")]
        public IEnumerable<AutocompleteModel>GetCustList()
        {
           return customerService.GetCustomerList();
        }


        [HttpPost]
        [Route("AddCustomer")]
        public IActionResult AddCustomer([FromBody]Customer customer)
        {
            if(customer==null)
            {
                return BadRequest("CustomerData Required");
            }
            if (customerService.Add(customer))
            {
                return Ok("Saved Successfully!");
            }
            else return StatusCode(500, "Failed to add product");
        }

        [HttpPut]
        [Route("Edit")]
        public IActionResult UpdateCustomer([FromBody]Customer customer)
        {
             if (customer == null)
            {
                return BadRequest("Customer details required");
            }
             if (customerService.Edit(customer))
            {
                return Ok("Edited Successfully!");
            }
            else return StatusCode(500, "Failed to edit customer");
          
    }


        [HttpDelete]
        [Route("Delete/{customer_id}")]
        public IActionResult DeleteCustomer(int customer_id){
            if (customerService.Delete(customer_id))
            {
                return Ok("Deleted Successfully!");
            }
            else return StatusCode(500, "Failed to delete customer");
        }

    }
}