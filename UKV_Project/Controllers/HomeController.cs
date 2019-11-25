using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UKV_Project.Models;
using Newtonsoft.Json;
using System.Net.Http;

namespace UKV_Project.Controllers
{
    public class HomeController : Controller
    {
        /*
        private List<Result> res;

        public HomeController()
        {
            res = new List<Result>()
        {
            new Result()
            { title ="a",name="Rakesh",state="Kalluri", agency="raki.kalluri@gmail.com", web_page="Nani.Kumar@gmail.com", telephone="Summit", fax="IT", email="BPO", seq_num=0 },
            new Result()
            { title ="a",name="Naresh",state="C", agency="Naresh.C@gmail.com", web_page="Nani.Kumar@gmail.com", telephone="IBM", fax="IT", email="BPO", seq_num=1 },
            new Result()
            { title ="a",name="Madhu",state="K", agency="Madhu.K@gmail.com", web_page="Nani.Kumar@gmail.com", telephone="HCl", fax="IT", email="BPO", seq_num=2 },
            new Result()
            { title ="a",name="Ali",state="MD", agency="Ali.MD@gmail.com", web_page="Nani.Kumar@gmail.com", telephone="Tech Mahindra", fax="BPO", email="BPO", seq_num=3 },
            new Result()
            { title ="a",name="Chithu",state="Raju", agency="Chithu.Raju@gmail.com", web_page="Nani.Kumar@gmail.com", telephone="Dell", fax="BPO", email="BPO", seq_num=4 },
            new Result()
            { title ="a",name="Nani",state="Kumar", agency="Nani.Kumar@gmail.com", web_page="Nani.Kumar@gmail.com", telephone="Infosys", fax="BPO", email="BPO", seq_num=5 },

        };
        }
        */

        string BASE_URL = "https://developer.nps.gov/api/v1/events";
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
            string IEXTrading_API_PATH = BASE_URL+ "?api_key=dCgbntSNVGwM7mzqC4hGhL6EGtp1pmYJDmhyuGnj";
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

        public IActionResult Index()
        {
            // Get the data from the List using GetSymbols method
            List<Datum> datums = GetData();
            // Send the data to the Index view
            return View(datums);
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
