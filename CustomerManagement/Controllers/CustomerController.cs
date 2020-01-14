using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using CustomerManagement.Models.Domain;
using CustomerManagement.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CustomerManagement.Controllers
{
    /// <summary>
    /// Provides the main API for customer management
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="customerService">An instance of ICustomerService</param>
        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        /// <summary>
        /// Search for a customer using a search term
        /// </summary>
        /// <param name="term">Search term containing part of First Name or Last Name</param>
        /// <returns>A list of matching customers' models</returns>
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<CustomerModel>> Find(string term)
        {
            return Ok(_customerService.Find(term));
        }

        /// <summary>
        /// Get a customer by its id
        /// </summary>
        /// <param name="id">Id of customer to be retrieved</param>
        /// <returns>Requested customer model</returns>
        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<CustomerModel> Get(int id)
        {
            var customer = _customerService.GetById(id);
            if (customer == null)
                return NotFound();

            return customer;
        }

        /// <summary>
        /// Update the customer info
        /// </summary>
        /// <param name="model">Customer model including new info</param>
        // POST api/values
        [HttpPost]
        public ActionResult Update(CustomerModel model)
        {
            if (!_customerService.Update(model))
                return BadRequest("Customer not found");

            return Ok();
        }

        /// <summary>
        /// Add a new customer
        /// </summary>
        /// <param name="model">Customer model to insert to DB</param>
        // PUT api/values
        [HttpPut]
        public ActionResult Add(CustomerModel model)
        {
            _customerService.Add(model);
            return Ok();
        }

        /// <summary>
        /// Delete a customer by id
        /// </summary>
        /// <param name="id">Id of customer to be deleted</param>
        // DELETE api/values/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            if (!_customerService.Delete(id))
                return BadRequest("Customer not found");

            return Ok();
        }
    }
}