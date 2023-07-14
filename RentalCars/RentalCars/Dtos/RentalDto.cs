using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentalCars.Dtos
{
    public class RentalDto
    {
        public int RentalID { get; set; }

        public CarDto Car { get; set; }
        public CustomerDto Customer { get; set; }

        public Nullable<System.DateTime> DateRented { get; set; }
        public Nullable<System.DateTime> DateReturned { get; set; }
    }
}