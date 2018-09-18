using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using aspnet_mysql.Models;
using log4net;

namespace aspnet_mysql.Controllers
{
    public class HomeController : Controller
    {
		private ILog log = LogManager.GetLogger(Startup.repository.Name, typeof(HomeController));

        public IActionResult Index()
        {
			log.Info("test");
            return View();
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
