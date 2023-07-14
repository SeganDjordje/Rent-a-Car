using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RentalCars.Dtos
{
    public class CustomerDto
    {
        public int CustomerID { get; set; }

        [Required]
        public string Name { get; set; }
        public string DriverLicNo { get; set; }
    }
}