using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RentalCars.Dtos
{
    public class CarDto
    {
        public int CarID { get; set; }

        [Required]
        public string Manufacturer { get; set; }
        [Required]
        public string Model { get; set; }
        [Required]
        public string LicencePlate { get; set; }

        [DefaultValue(true)]
        public bool Available { get; set; }
        public Nullable<System.DateTime> Year { get; set; }

    }
}