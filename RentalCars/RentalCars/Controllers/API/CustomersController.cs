using AutoMapper;
using RentalCars.Dtos;
using RentalCars.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RentalCars.Controllers.API
{
    public class CustomersController : ApiController
    {
        // Here we can find customers property (plural)
        // Declaring this field, the CustomersController class can have access to the database context/data store.
        // Now the controller can performe operations like retreving, creating, updating and deleting cars.
        private RentalCarsEntities _context;

        public CustomersController() // Constructor
        {
            // This ensures that each instance of the CustomersController class has it's own dedicated data contex for performing operations.
            _context = new RentalCarsEntities();
        }





        // GET/api/customers
        // DTO Edited
        // Retreves all customers from the data store/database. It returns an enumartion of Customer objects, representing all the Customer.
        public IEnumerable<CustomerDto> GetCustomers()
        {
            // Retrives all Customer objects from the database(_context)
            // Maps/copies the Customer object to CustomerDto and returns a list of CustomerDto objects.
            return _context.Customers.ToList().Select(Mapper.Map<Customer, CustomerDto>);
        }

        // GET/api/customers/1
        // DTO Edited
        // Retreves a specific customer with ID = 1.
        public IHttpActionResult GetCustomer(int id) 
        {
            var customer = _context.Customers.SingleOrDefault(c => c.CustomerID == id); // Finds the existing customer with specific id from the database/context.Customers

            if (customer == null)
               return NotFound(); // If customer is null then return NotFound.


            // Maps the customer object of type Customer to CustomerDto and returns CustomerDto
            return Ok(Mapper.Map<Customer, CustomerDto>(customer));
        }




        // POST/api/customers
        // DTO Edited
        [HttpPost] // Need to set this request.
        public IHttpActionResult CreateCustomer(CustomerDto customerDto) // Recevies a customer object , adds it to _context/data store and saves changes.
        {
            if (!ModelState.IsValid) // If it is not valid trows an exception.
                return BadRequest();

            // Maps the customerDto object of type CustomerDto to corresponding properties of
            // customer object of type Customer.
            var customer = Mapper.Map<CustomerDto, Customer>(customerDto);

            _context.Customers.Add(customer); // If valid adds the car to the data store.
            _context.SaveChanges(); // Saves


            // Assigns the CustomerID of the newly created rental to the corresponding propertie in the customerDto object.
            // This ensures that the CustomerID is updated with generated value from the database.
            customerDto.CustomerID = customer.CustomerID;
            return Created(new Uri(Request.RequestUri + "/" + customer.CustomerID), customerDto); // Returns a respose indicating that a Rental has been created.
        }




        // PUT/api/customers/1
        // DTO Edited
        [HttpPut] // Need to set this request.
        // Updates the details of a specific customer. It recives a Customer object representng the updated details of the customer.
        public void UpdateCustomer(int id, CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var customerInDB = _context.Customers.SingleOrDefault(c => c.CustomerID == id); // Retreves the existing customer from the database/context.Customers
            if (customerInDB == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            // Updates the properties with values from customer object.
            customerInDB.Name = customerDto.Name;
            customerInDB.DriverLicNo = customerDto.DriverLicNo;


            Mapper.Map(customerDto, customerInDB); // Maps the customerDto to customerInDB.
            _context.SaveChanges();
        }





        // DELETE/api/1
        [HttpDelete]
        // Deletes a customer from the database
        public void DeleteCustomer(int id)
        {
            var customerInDB = _context.Customers.SingleOrDefault(c => c.CustomerID == id); // Retreves the existing customer from the database/context.Customers
            if (customerInDB == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _context.Customers.Remove(customerInDB); // Removes
            _context.SaveChanges(); // Saves
        }

    }
}
