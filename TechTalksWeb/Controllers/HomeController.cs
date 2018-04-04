using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TechTalksWeb.Models;

namespace TechTalksWeb.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult DisplayTechTalksResult(string searchText, string serviceUrl)
        {
            // string url = "http://techtalksapi:8080/api/keyvalue";
            // string url = serviceUrl.TrimEnd('/') + "/" + searchText;

            // techtalksapi.abc2018sg:8080

            // string output = url;
            string output = serviceUrl;

            List<TechTalk> techTalks = new List<TechTalk>();
            try
            {
                var client = new WebClient();
                var response = client.DownloadString(serviceUrl);
                dynamic jsonData = JsonConvert.SerializeObject(response);
                techTalks.AddRange(JsonConvert.DeserializeObject<List<TechTalk>>(jsonData));
            }
            catch (Exception)
            {
                output = "not found";
            }

            var result = new List<TechTalk> 
            {
                new TechTalk {Id = 1, Name="Docker", Category = 1},
                new TechTalk {Id = 2, Name="Kubernetes", Category = 2}
            };

            techTalks.AddRange(result);

            return PartialView("SearchServiceResults", result);

        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
