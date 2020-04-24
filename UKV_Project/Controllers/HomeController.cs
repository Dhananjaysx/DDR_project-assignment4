using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DDRProject.Models;
using Newtonsoft.Json;
using System.Net.Http;

namespace DDRProject.Controllers
{
    public class HomeController : Controller
    {

        string BASE_URL = "https://developer.nps.gov/api/v1/";
        HttpClient httpClient;

        public HomeController()
        {
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new
                System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        /*
            Calls the IEX reference API to get the list of symbols.
            Returns a list of the companies whose information is available. 
        */
        public List<Datum> GetData()
        {
            string IEXTrading_API_PATH = BASE_URL + "events?api_key=dCgbntSNVGwM7mzqC4hGhL6EGtp1pmYJDmhyuGnj";
            string datumList = "";
            RootObject datums = null;

            // Connect to the IEXTrading API and retrieve information
            httpClient.BaseAddress = new Uri(IEXTrading_API_PATH);
            HttpResponseMessage response = httpClient.GetAsync(IEXTrading_API_PATH).GetAwaiter().GetResult();

            // Read the Json objects in the API response
            if (response.IsSuccessStatusCode)
            {
                datumList = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            }

            // Parse the Json strings as C# objects
            if (!datumList.Equals(""))
            {
                datums = JsonConvert.DeserializeObject<RootObject>(datumList);
                //datums = datums.GetRange(0, 50);
            }

            return datums.data;
        }

        public List<Datum2> GetData2()
        {
            string IEXTrading_API_PATH = BASE_URL + "campgrounds?api_key=dCgbntSNVGwM7mzqC4hGhL6EGtp1pmYJDmhyuGnj";
            string datum2List = "";
            RootObject2 datums2 = null;

            // Connect to the IEXTrading API and retrieve information
            httpClient.BaseAddress = new Uri(IEXTrading_API_PATH);
            HttpResponseMessage response = httpClient.GetAsync(IEXTrading_API_PATH).GetAwaiter().GetResult();

            // Read the Json objects in the API response
            if (response.IsSuccessStatusCode)
            {
                datum2List = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            }

            // Parse the Json strings as C# objects
            if (!datum2List.Equals(""))
            {
                datums2 = JsonConvert.DeserializeObject<RootObject2>(datum2List);
                //datums = datums.GetRange(0, 50);
            }

            return datums2.data;
        }

        public IActionResult Index()
        {
            // Get the data from the List using GetSymbols method
            //List<Datum> datums = GetData();
            // Send the data to the Index view
            //return View(datums);
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

       public IActionResult Contact()
        {
           // List<Datum> datums = GetData();
            //return View(datums);
            return View();
        }
        
        public IActionResult Visit()
        {
            // List<Datum2> datums2 = GetData2();
            //return View(datums2);
            return View();
        }

       /* public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult More()
        {
            return View();
        }*/
    }
}