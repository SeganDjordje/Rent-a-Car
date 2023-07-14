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
    public class CarsController : ApiController
    {
        // Here we can find cars property (plural)
        // Declaring this field, the CarsController class can have access to the database context/data store.
        // Now the controller can performe operations like retreving, creating, updating and deleting cars.
        private RentalCarsEntities _context;

        public CarsController() // Constructor
        {
            // This ensures that each instance of the CarsController class has it's own dedicated data contex for performing operations.
            _context = new RentalCarsEntities();
        }





        // GET/api/cars
        // DTO Edited
        // Retreves all cars from the data store/database. It returns an enumartion of Car objects, representing all the cars.
        public IEnumerable<CarDto> GetCars()
        {
            // Retrives all Car objects from the database(_context),
            // Maps/copies the Car object to CarDto and returns a list of CarDto objects.
            return _context.Cars.ToList().Select(Mapper.Map<Car, CarDto>);
        }


        // GET/api/cars/1
        // DTO Edited
        // Retreves a specific Car based on the provided id. 
        public IHttpActionResult GetCar(int id)
        {
            var car = _context.Cars.SingleOrDefault(g => g.CarID == id); // Finds the existing car with specific id from the database/context.Cars

            if (car == null)
                return NotFound(); // If car is null then return NotFound.

            // Maps the car object of type Car to CarDto and returns CarDto
            return Ok(Mapper.Map<Car, CarDto>(car));
        }





        // POST/api/cars
        // DTO Edited
        // Recevies a car object , adds it to _context/data store and saves changes. 
        [HttpPost] // Need to set this request.
        public IHttpActionResult CreateCar(CarDto carDto)
        {
            if (!ModelState.IsValid) // If it is not valid trows an exception.
                return BadRequest();

            // Maps the carDto object of type CarDto to corresponding properties of car object of type Car.
            var car = Mapper.Map<CarDto, Car>(carDto);

            _context.Cars.Add(car); // If valid adds the car to the data store.
            _context.SaveChanges(); // Saves

            // Assigns the CarID of the newly created car to the corresponding propertie in the carDto object.
            // This ensures that the CarID is updated with generated value from the database.
            carDto.CarID = car.CarID; 
            return Created(new Uri(Request.RequestUri + "/" + car.CarID), carDto); // Returns a respose indicating that a Car has been created.
        }




        // PUT/api/cars/1
        // DTO Edited
        // Updates the details of a specific car. It recives a Car object representng the updated details of the car.
        [HttpPut] // Need to set this request.
        public void UpdateCar(int id, CarDto carDto)
        {
            if (!ModelState.IsValid) // Checks the model state 
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var carInDb = _context.Cars.SingleOrDefault(g => g.CarID == id); // Finds the existing car with specific id from the database/context.Cars
            if(carInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            // Updates the properties with values from car object.
            carInDb.Manufacturer = carDto.Manufacturer;
            carInDb.Model = carDto.Model;
            carInDb.LicencePlate = carDto.LicencePlate;

            carInDb.Year = carDto.Year;
            carInDb.Available = carDto.Available;

            Mapper.Map(carDto, carInDb); // Maps the carDto to carInDb.
            _context.SaveChanges(); // Saves database
        }





        // DELETE/api/cars/1
        // Deletes a Car from the database.
        [HttpDelete] // Need to set this request.
        public void DeleteCars(int id)
        {
            var carInDb = _context.Cars.SingleOrDefault(g => g.CarID == id); // Finds the existing car with specific id from the database/context.Cars

            if (carInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _context.Cars.Remove(carInDb); // Removes
            _context.SaveChanges(); // Saves
        }
    }
}
