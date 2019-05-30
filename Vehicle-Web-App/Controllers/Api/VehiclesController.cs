using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vehicle_Web_App.Models;
using Vehicle_Web_App.Dtos;
using AutoMapper;
using System.Web.Mvc.Html;

namespace Vehicle_Web_App.Controllers.Api
{
    public class VehiclesController : ApiController
    {
        private ApplicationDbContext _context;
        public VehiclesController()
        {
            _context = new ApplicationDbContext();
        }
        public IHttpActionResult GetVehicles()
        {
           var vehicleDtos =  _context.Vehicles.ToList().Select(Mapper.Map<Vehicle,VehicleDto>);
            return Ok(vehicleDtos);
        }

        public IHttpActionResult GetVehicle(int id)
        {
            var vehicle = _context.Vehicles.SingleOrDefault(c => c.Id == id);
            if (vehicle == null)
                return NotFound();
            return Ok(Mapper.Map<Vehicle, VehicleDto>(vehicle));

        }

      

        [HttpPost]
        public IHttpActionResult CreateVehicle(VehicleDto vehicleDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();


            var vehicle = Mapper.Map<VehicleDto, Vehicle>(vehicleDto);


            _context.Vehicles.Add(vehicle);
            _context.SaveChanges();

            vehicleDto.Id = vehicle.Id;
            
            

            return Created(new Uri(Request.RequestUri + "/" + vehicle.Id),vehicleDto);

            
        }
        [HttpPut]
        public IHttpActionResult UpdateVehicle(int id, VehicleDto vehicleDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var vehicleInDb = _context.Vehicles.SingleOrDefault(c => c.Id == id);
            if (vehicleInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            Mapper.Map<VehicleDto, Vehicle>(vehicleDto, vehicleInDb);
            
                       _context.SaveChanges();
            return Ok();

            
        }
        [HttpDelete]
        public IHttpActionResult DeleteVehicle(int id)
        {
                       var vehicleInDb = _context.Vehicles.SingleOrDefault(c => c.Id == id);
            if (vehicleInDb == null)
                return NotFound();
            _context.Vehicles.Remove(vehicleInDb);
            _context.SaveChanges();
            return Ok();


        }
      
    }
}
