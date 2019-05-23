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

        string Baseurl = "http://localhost:51570/";
        public async Task<ActionResult> Index()
        {
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

        public ActionResult Details(int id)
        {
            var vehicle = _context.Vehicles.SingleOrDefault(c => c.Id == id);

            if (vehicle == null)
                return HttpNotFound();

            return View(vehicle);
        }
    }
}