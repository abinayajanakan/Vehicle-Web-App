using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vehicle_Web_App.Models;

using System.Web.Mvc.Html;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Vehicle_Web_App.ViewModel;
using Vehicle_Web_App.Dtos;
using AutoMapper;
using System.Data.Entity;

namespace Vehicle_Web_App.Controllers
{
    public class VehiclesController : Controller
    {

        private ApplicationDbContext _context;

        public VehiclesController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Vehicles
        public ActionResult New()
        {

            return View();
        }


       

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Vehicle vehicle, VehicleDto vehicleDto)
        {

            if (vehicle.Id == 0)
                _context.Vehicles.Add(vehicle);
            else
            {


                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:51570/api/");
                    var vehicleInDb = _context.Vehicles.Single(c => c.Id == vehicle.Id);

                    //HTTP POST
                    var putTask = client.PutAsJsonAsync<Vehicle>("vehicles/" + vehicle.Id + "/", vehicle);
                    putTask.Wait();

                    var result = putTask.Result;
                    if (result.IsSuccessStatusCode)
                    {

                        return RedirectToAction("Index");

                    }
                }
            }
            return RedirectToAction("Index");


        }


        public ActionResult Edit(int id)
        {
            var vehicle = _context.Vehicles.SingleOrDefault(c => c.Id == id);

            if (vehicle == null)
                return HttpNotFound();

            var viewModel = new VehicleViewModel
            {
                Vehicle = vehicle

            };

            return View(viewModel);
        }

                              
            public async Task<ActionResult> Index(string searchString)
            {


            var vehicles = from s in _context.Vehicles
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                vehicles = vehicles.Where(s => s.Make.Contains(searchString)
                                       || s.Model.Contains(searchString));

                return View(await vehicles.ToListAsync());
            }
            string Baseurl = "http://localhost:51570/";
            List<Vehicle> vehicleInfo = new List<Vehicle>();

                using (var client = new HttpClient())
                {
                    //Passing service base url  
                    client.BaseAddress = new Uri(Baseurl);

                    client.DefaultRequestHeaders.Clear();
                    //Define request data format  
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                    HttpResponseMessage Res = await client.GetAsync("api/vehicles");

                    //Checking the response is successful or not which is sent using HttpClient  
                    if (Res.IsSuccessStatusCode)
                    {
                        //Storing the response details recieved from web api   
                        var vehicleResponse = Res.Content.ReadAsStringAsync().Result;

                        //Deserializing the response recieved from web api and storing into the Employee list  
                        vehicleInfo = JsonConvert.DeserializeObject<List<Vehicle>>(vehicleResponse);

                    }
                    //returning the employee list to view  
                    return View(vehicleInfo);
                }
            }

         
    }
}
