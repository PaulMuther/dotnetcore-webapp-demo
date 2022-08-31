using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Dynamic;
using System.Text.Encodings.Web;


namespace MvcMovie.Controllers
{
    public class HelloWorldController : Controller
    {
        // 
        // GET: /HelloWorld/

        public async Task<IActionResult> Index()
        {
            //call out to minimal api
            using var client = new HttpClient();
            var result = await client.GetStringAsync("https://red-cus-sandbox-minimal-api.azurewebsites.net/");
            //dynamic? dynamicObj = (dynamic?)JsonConvert.DeserializeObject<dynamic>(result);
            var dynamicObj = JsonConvert.DeserializeAnonymousType(result, new {
                PrivateIp = string.Empty,
                HostName = string.Empty,
                ICanHazIp = string.Empty,
                RemoteIp = string.Empty,
                LocalIp = string.Empty,
                HeadersCommaDelimited = string.Empty
            });

            ViewData["PrivateIp"] = dynamicObj?.PrivateIp;
            ViewData["HostName"] = dynamicObj?.HostName;
            ViewData["ICanHazIp"] = dynamicObj?.ICanHazIp;
            ViewData["RemoteIp"] = dynamicObj?.RemoteIp;
            ViewData["LocalIp"] = dynamicObj?.LocalIp;
            ViewData["HeadersCommaDelimited"] = dynamicObj?.HeadersCommaDelimited;

            return View();
        }

        // 
        // GET: /HelloWorld/Welcome/ 

        public IActionResult Welcome(string name, int numTimes = 1)
        {
            ViewData["Message"] = "Hello " + name;
            ViewData["NumTimes"] = numTimes;

            return View();
        }
    }
}