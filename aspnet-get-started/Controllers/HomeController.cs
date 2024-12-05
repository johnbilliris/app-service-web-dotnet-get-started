using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;
using System.Net.Http;
using System.Configuration;
using System.Threading.Tasks;
using System.Text;
using aspnet_get_started.Models;
using Newtonsoft.Json;

namespace aspnet_get_started.Controllers
{
    public class HomeController : Controller
    {
        private Stopwatch stopwatch = new Stopwatch();
        private long actualDelay = 0;
        private HttpClient client = null;

        public HomeController()
        {
            client = new HttpClient() { BaseAddress = new Uri(ConfigurationManager.AppSettings["ToDoApiUrl"]) };
            stopwatch.Start();
        }

        public async Task<ActionResult> Index()
        {
            HttpResponseMessage response = await client.GetAsync("/api/todo");
            stopwatch.Stop();
            ViewBag.ResponseDelay = stopwatch.ElapsedMilliseconds;
            return View();
        }

        public async Task<ActionResult> About()
        {
            ViewBag.Message = "Your application description page.";
            HttpResponseMessage response = await client.GetAsync("/api/todo/1");
            stopwatch.Stop();
            ViewBag.ResponseDelay = stopwatch.ElapsedMilliseconds;
            return View();
        }

        public async Task<ActionResult> Contact()
        {
            ViewBag.Message = "Your contact page.";

            Random random = new Random();
            long id = (long)random.Next(1, int.MaxValue);


            TodoItem item = new TodoItem() { Name = String.Format("Test Item {0}", id), IsComplete = false, Id= id };
            string str = JsonConvert.SerializeObject(item);
            StringContent content = new StringContent(str, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(
                                                    "/api/Todo",
                                                    content
                                                    );
            stopwatch.Stop();
            ViewBag.ResponseDelay = stopwatch.ElapsedMilliseconds;
            return View();
        }
    }
}