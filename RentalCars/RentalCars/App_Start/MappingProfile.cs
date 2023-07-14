using AutoMapper;
using RentalCars.Dtos;
using RentalCars.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentalCars.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Allows bidirectional mapping
            // CarDto
            Mapper.CreateMap<Car, CarDto>();
            Mapper.CreateMap<CarDto, Car>();

            // CustomerDto
            Mapper.CreateMap<Customer, CustomerDto>();
            Mapper.CreateMap<CustomerDto, Customer >();

            // RentalDto
            Mapper.CreateMap<Rental, RentalDto>();
            Mapper.CreateMap<RentalDto, Rental>();
        }
    }
}