using ConfigDemo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        IOptionsMonitor<AliOSSConfig> optionsMonitor;
        public HomeController(ILogger<HomeController> logger, IOptionsMonitor<AliOSSConfig> optionsMonitor)
        {
            _logger = logger;
            this.optionsMonitor = optionsMonitor;
            this.optionsMonitor.OnChange(_ => Console.WriteLine(_.BucketName));
        }

        public IActionResult Index()
        {
            return Json(optionsMonitor.CurrentValue);
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
