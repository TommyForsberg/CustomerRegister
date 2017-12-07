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
        // GET: api/values
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(databaseContext.Customers);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post(Customer customer)
        {
            return Ok(customer);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
