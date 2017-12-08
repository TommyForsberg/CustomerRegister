using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CustomerRegister.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CustomerRegister
{
    [Route("api/[controller]")]
    public class CustomersController : Controller
    {
        private readonly ILogger<CustomersController> logger;
        private DBContext databaseContext;

        public CustomersController(DBContext databaseContext, ILogger<CustomersController> logger)
        {
            this.databaseContext = databaseContext;
            this.logger = logger;
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
        public IActionResult AddCustomer(Customer customer)
        {
            if (!ModelState.IsValid)
                return BadRequest(customer);

            
            databaseContext.Add(customer);
            databaseContext.SaveChanges();
            logger.LogInformation("A customer was added");
            return Ok(databaseContext.Customers);
        }

        [HttpPut]
        public IActionResult Update(Customer customer)
        {
            if (!ModelState.IsValid)
                return BadRequest(customer);
            customer.Updated = true;
            databaseContext.Update(customer);
            databaseContext.SaveChanges();
            return Ok(databaseContext.Customers);
        }


        [HttpDelete]
        public IActionResult RemoveCustomer(int id)
        {
            databaseContext.Remove(databaseContext.Customers.Find(id));
            databaseContext.SaveChanges();
            return Ok(databaseContext.Customers);
        }

        [HttpDelete("deleteall")]
        public IActionResult DeleteAll()
        {
            databaseContext.RemoveRange(databaseContext.Customers);
            databaseContext.SaveChanges();
            return Ok(databaseContext.Customers);
        }
        [HttpGet("seelogs")]
        public IActionResult SeeLogs()
        {
            var file = Path.Combine(Environment.CurrentDirectory, "logs", "log.txt");
            var lines = System.IO.File.ReadAllLines(file);
            return Ok(lines);
        }
        [HttpGet("SeedDatabase")]
        public IActionResult SeedDatabase()
        {
            databaseContext.Customers.RemoveRange(databaseContext.Customers);
            var file = Path.Combine(Environment.CurrentDirectory, "data", "PersonExtra.csv");
            using (var streamReader = System.IO.File.OpenText(file))
            {

                while (!streamReader.EndOfStream)
                {
                    var line = streamReader.ReadLine();
                    var data = line.Split(new[] { ',' });
                    var customer = new Customer() { FirstName = data[1], LastName = data[2], Email = data[3], Gender = data[4], Age = int.Parse(data[5]) };
                    databaseContext.Add(customer);
                }

                databaseContext.SaveChanges();
            }
            return Ok(databaseContext.Customers);
        }
    }
}
