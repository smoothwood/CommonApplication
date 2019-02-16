using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Web.Models;

namespace Web.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("[controller]")]
    public class HomeController : Controller
    {
        /// <summary>
        /// Demo for upload, take a look
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Index")]
        public IActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// You DON'T need to bother with this action
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Privacy")]
        public IActionResult Privacy()
        {
            return View();
        }
        /// <summary>
        /// You DON'T need to bother with this action
        /// </summary>
        /// <returns></returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [HttpGet]
        [Route("Error")]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
