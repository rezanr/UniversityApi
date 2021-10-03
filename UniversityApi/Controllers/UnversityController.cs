using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using UniversityApi.Data;
using UniversityApi.Models;

namespace UniversityApi.Controllers
{
    public class UnversityController : Controller
    {
        private AppicationDataContext appicationDataContext;

        public UnversityController(AppicationDataContext context)
        {
            appicationDataContext = context;
        }
        public IActionResult Index()
        {
            var result = appicationDataContext.unversities.ToList();
            //ViewData["unversities"] = result;
            ViewBag.unversities = result;
            return View();
        }

        [HttpGet("api/add")]
        public async Task<IActionResult> GetDataFromApi()
        {
            //Make a HTTPClient
            var Client = new HttpClient();
            var getRequest = await Client.GetAsync("http://universities.hipolabs.com/search?country=Denmark");
            var content = getRequest.Content.ReadAsStringAsync().Result;
            var listOfUnversities = JsonConvert.DeserializeObject<List<Unversity>>(content);

            appicationDataContext.unversities.AddRange(listOfUnversities);
            appicationDataContext.SaveChanges();

            return Ok("Database has been updated");

        }
    }
}
