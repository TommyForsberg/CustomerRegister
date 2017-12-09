using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CustomerRegister.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Targets;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CustomerRegister
{
    [Route("api/[controller]")]
    public class CustomersController : Controller
    {
        private readonly ILogger<CustomersController> logger;
        private DBContext databaseContext;


        /// <summary>
        /// Wraps the list of customers from context for easy logging with every fetch.
        /// </summary>
        public List<Customer> Customers
        {
            get
            {
                logger.LogInformation("All customers were fetched from the database.");
                return databaseContext.Customers.ToList();
            }
        }


        public CustomersController(DBContext databaseContext, ILogger<CustomersController> logger)
        {
            this.databaseContext = databaseContext;
            this.logger = logger;
        }

        [HttpGet]
        public IActionResult GetAllCustomers()
        {
            return Ok(Customers);

        }

        [HttpGet("GetCustomer")]
        public IActionResult GetCustomer(int id)
        {
            var customer = Customers
                .Where(o => o.Id.Equals(id))
                .FirstOrDefault();

            if(customer == null)
                return NotFound();

            return Ok(customer);         
        }

        // POST Customer to database.
        [HttpPost]
        public IActionResult AddCustomer(Customer customer)
        {
            if (!ModelState.IsValid)
                return BadRequest(customer);

            databaseContext.Add(customer);
            databaseContext.SaveChanges();
            logger.LogInformation("A customer with name: " +customer.FirstName + " " + customer.LastName + " was added.");
            return Ok(Customers);
        }

        //Update customer in database.
        [HttpPut]
        public IActionResult Update(Customer customer)
        {
            if (!ModelState.IsValid)
                return BadRequest(customer);

            customer.Updated = true;
            databaseContext.Update(customer);
            databaseContext.SaveChanges();
            logger.LogInformation("Customer with Id "+customer.Id + " was updated.");
            return Ok(Customers);
        }

        [HttpDelete]
        public IActionResult RemoveCustomer(int id)
        {
            var customer = Customers
               .Where(o => o.Id.Equals(id))
               .FirstOrDefault();

            if (customer == null)
                return NotFound(Customers);

            databaseContext.Remove(databaseContext.Customers.Find(id));
            databaseContext.SaveChanges();
            return Ok(Customers);
        }

        [HttpDelete("deleteall")]
        public IActionResult DeleteAll()
        {
            databaseContext.RemoveRange(databaseContext.Customers);
            databaseContext.SaveChanges();
            logger.LogInformation("All customers were deleted from the database.");

            return Ok(Customers);
        }
        [HttpGet("GetLogsByDate/{date}")]
        public IActionResult GetLogsByDate(DateTime date)
        {
            var fileTarget = (FileTarget)LogManager.Configuration.FindTargetByName("ownFile-web");
            var logEventInfo = new LogEventInfo { TimeStamp = date };
            string fileName = fileTarget.FileName.Render(logEventInfo);

            if (!System.IO.File.Exists(fileName))
                return NotFound();

            var log = System.IO.File.ReadAllText(fileName);
            return Ok(log);
        }
        [HttpGet("SeedDatabase")]
        public IActionResult SeedDatabase()
        {
            databaseContext.Customers.RemoveRange(databaseContext.Customers);
            var file = Path.Combine(Environment.CurrentDirectory, "data", "PersonShort.csv");

            if (!System.IO.File.Exists(file))
                return NotFound();

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
                logger.LogInformation("The database was seeded from textfile.");
            }
            return Ok(Customers);
        }
    }
}
