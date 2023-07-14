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
    public class RentalsController : ApiController
    {
        // Here we can find rentals property (plural)
        // Declaring this field, the RentalsController class can have access to the database context/data store.
        // Now the controller can performe operations like retreving, creating, updating and deleting rentals.
        public RentalCarsEntities _context;

        public RentalsController() // Constructor
        {
            // This ensures that each instance of the RentalsController class has it's own dedicated data contex for performing operations.
            _context = new RentalCarsEntities();
        }





        // DTO Edited
        // GET/api/rentals
        // Retreves all rentals from the data store/database. It returns an enumartion of Rental objects, representing all the rentals.
        public IEnumerable<RentalDto> GetRentals()
        {
            // Retrives all Rental objects from the database(_context)
            // Maps/copies the Rental object to CarDto and returns a list of RentalDto objects.
            return _context.Rentals.ToList().Select(Mapper.Map<Rental, RentalDto>);
        }


        // DTO Edited
        // GET/api/rentals/1
        // Retreves a specific Customer based on the provided id. 
        public IHttpActionResult GetRental(int id)
        {
            var rental = _context.Rentals.SingleOrDefault(r => r.RentalID == id); // Finds the existing rental with specific id from the database/context.Rentals

            if (rental == null)
                return NotFound();

            // Maps the rental object of type Rental to RentalDto and returns RentalDto
            return Ok(Mapper.Map<Rental, RentalDto>(rental));
        }



        // DTO Edited
        // POST/api/rentals
        [HttpPost] //Need to set this request.
        public IHttpActionResult CreateRental(RentalDto rentalDto) // Recevies a rental object , adds it to _context/data store and saves changes. 
        {
            if (!ModelState.IsValid) // If it is not valid trows an exception.
                return BadRequest();

            // Maps the rentalDto object of type RentalDto to corresponding properties of rental object of type Rental.
            var rental = Mapper.Map<RentalDto, Rental>(rentalDto);

            _context.Rentals.Add(rental); // If valid adds the rental to the data store.
            _context.SaveChanges(); // Saves

            // Assigns the RentalID of the newly created rental to the corresponding propertie in the rentalDto object.
            // This ensures that the RentalID is updated with generated value from the database.
            rentalDto.RentalID = rental.RentalID;
            return Created(new Uri(Request.RequestUri + "/" + rental.RentalID), rentalDto); // Returns a respose indicating that a Rental has been created.
        }




        // DTO Edited
        // PUT/api/rentals/1
        [HttpPut] // Need to set this request.
        // Updates the details of a specific rental. It recives a Rental object representng the updated details of the rental.
        public void UpdateRental(int id, RentalDto rentalDto)
        {
            if (!ModelState.IsValid) // Checks the model state 
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var rentalInDb = _context.Rentals.SingleOrDefault(r => r.RentalID == id); // Finds the existing Rental with specific id from the database/context.Rentals
            if (rentalInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            // Updates the properties with values from car object.
            rentalInDb.DateRented = rentalDto.DateRented;
            rentalInDb.DateReturned = rentalDto.DateReturned;

            Mapper.Map(rentalDto, rentalInDb); // Maps the rentalDto to rentalInDb.
            _context.SaveChanges(); // Saves database
        }





        // DELETE/api/rentals/1
        [HttpDelete] // Need to set this request.
        // Deletes a Rental from the database.
        public void DeleteRental(int id)
        {
            var rentalInDb = _context.Rentals.SingleOrDefault(r => r.RentalID == id); // Retreves the existing rental from the database/context.Rentals

            if (rentalInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _context.Rentals.Remove(rentalInDb); // Removes
            _context.SaveChanges(); // Saves
        }
    }
}
