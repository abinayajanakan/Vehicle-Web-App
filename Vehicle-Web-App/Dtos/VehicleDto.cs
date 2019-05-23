using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vehicle_Web_App.Dtos
{
    public class VehicleDto
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }

    }
}