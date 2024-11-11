using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace aspnet_get_started.Controllers
{
    public enum ResponseMode
    {
        Constant = 0,
        Variable = 1
    }

    public class HomeController : Controller
    {
        private readonly ResponseMode responseMode = ResponseMode.Constant;
        private long responseDelay = 300;

        private Stopwatch stopwatch = new Stopwatch();
        private long actualDelay = 0;

        public HomeController()
        {
            try
            {
                responseMode = (ResponseMode)Enum.Parse(typeof(ResponseMode), ConfigurationManager.AppSettings["ResponseMode"]);
                responseDelay = long.Parse(ConfigurationManager.AppSettings["ResponseDelay"]);
            }
            catch (Exception)
            {
            }
            CauseResponseDelay();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        private void CauseResponseDelay()
        {
            stopwatch.Start();
            if (responseMode == ResponseMode.Constant)
            {
                while (stopwatch.ElapsedMilliseconds < responseDelay)
                {
                    int dummyInteger = 1;
                    dummyInteger += 1;
                }
            }
            else
            {
                Random random = new Random();
                responseDelay = random.Next(100, 500);
                while (stopwatch.ElapsedMilliseconds < responseDelay)
                {
                    // Do nothing
                }
            }
            stopwatch.Stop();
            actualDelay = stopwatch.ElapsedMilliseconds;

            ViewBag.ResponseMode = responseMode.ToString();
            ViewBag.ResponseDelay = responseDelay;
            ViewBag.ActualDelay = actualDelay;
        }
    }
}