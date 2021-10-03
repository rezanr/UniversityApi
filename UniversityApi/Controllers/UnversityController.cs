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
        private readonly AppicationDataContext appicationDataContext;

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
        [HttpGet("api/get")]
        public async Task<IActionResult> GetAsync()
        {
            // Get data from public API and add to our database
            var client = new HttpClient();
            var getRequest = await client.GetAsync("http://universities.hipolabs.com/search?country=Denmark");
            var content = getRequest.Content.ReadAsStringAsync().Result;
            var universtiesList = JsonConvert.DeserializeObject<List<Unversity>>(content);
            appicationDataContext.unversities.AddRange(universtiesList);
            appicationDataContext.SaveChanges();

            return Ok("Database has been updated");
        }

        
        public async Task<IActionResult> apiSource()
        {
            // Get data from public API
            var client = new HttpClient();
            var getRequest = await client.GetAsync("http://universities.hipolabs.com/search?country=Denmark");
            var content = getRequest.Content.ReadAsStringAsync().Result;
            var universtiesList = JsonConvert.DeserializeObject<List<Unversity>>(content);
            return View(universtiesList);

        }


    }
}
