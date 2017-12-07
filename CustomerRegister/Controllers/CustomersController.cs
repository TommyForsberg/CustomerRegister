using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerRegister.Models.Entities;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CustomerRegister
{
    [Route("api/[controller]")]
    public class CustomersController : Controller
    {

        private DBContext databaseContext;

        public CustomersController(DBContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }
       
        [HttpGet]
        public IActionResult GetAllCustomers()
        {
            return Ok(databaseContext.Customers);

        }

        [HttpGet("GetCustomer")]
        public IActionResult GetCustomer(int id)
        {

            return Ok(databaseContext.Customers.Find(id));
        }

        // POST Customer to database.
        [HttpPost]
        public IActionResult Post(Customer customer)
        {
            if (!ModelState.IsValid)
                return BadRequest(customer);

                databaseContext.Add(customer);
            databaseContext.SaveChanges();
            return Ok(databaseContext.Customers);
        }

        [HttpPut]
        public IActionResult Update(Customer customer)
        {
            databaseContext.Update(customer);
            databaseContext.SaveChanges();
            return Ok(databaseContext.Customers);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
