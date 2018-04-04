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
            string url = serviceUrl.TrimEnd('/') + "/" + searchText;

            // techtalksapi.abc2018sg:8080

            string output = url;

            try
            {
                var client = new WebClient();
                client.Headers.Add("User-Agent", "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_13_3) AppleWebKit/604.5.6 (KHTML, like Gecko) Version/11.0.3 Safari/604.5.6");
                var response = client.DownloadString(url);
                dynamic jsonData = JsonConvert.SerializeObject(response);
            }
            catch (Exception)
            {
                output = "not found";
            }

            var result = new {Key = searchText, Value = output};

            return PartialView("ServiceSearchResults", result);

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
