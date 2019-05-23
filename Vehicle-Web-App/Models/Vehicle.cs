using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc.Html;


namespace Vehicle_Web_App.Models
{
    public class Vehicle
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }

    }
}