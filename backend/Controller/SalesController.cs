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
    public class SalesController : ControllerBase
    {
         private readonly ISalesService salesService;

        public SalesController(ISalesService saleService)
        {
            salesService = saleService;
        }

        [HttpGet]
        [Route("GetSales")]
        public IEnumerable<SalesViewModel> Get()
        {
           return salesService.GetList();
        }

       
        [HttpPost]
        [Route("AddSales")]
        public IActionResult AddSales([FromBody] Sales sales)
        {

            if (sales == null)
            {
                return BadRequest("Product data is required");
            }
             if (salesService.Add(sales))
            {
                return Ok("Saved Successfully!");
            }
            else return StatusCode(500, "Failed to add sales");
           
        }


        [HttpPut]
        [Route("Edit")]
        public IActionResult EditSales([FromBody]Sales sales)
        {
            if(sales==null){
                return BadRequest("Sales Data Required");
            }
            if (salesService.Edit(sales)){
                return Ok("Edited Successfully!");
            }
            else return StatusCode(500, "Failed to edit sales");

        }


        [HttpDelete]
        [Route("Delete/{sales_id}")]
        public IActionResult DeleteProduct(int sales_id){
            if (salesService.Delete(sales_id))
            {
                return Ok("Deleted Successfully!");
            }
            else return StatusCode(500, "Failed to delete sales");

        }
    }
}


