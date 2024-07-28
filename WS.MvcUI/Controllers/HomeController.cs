using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WS.MvcUI.Models;

namespace WS.MvcUI.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {



            return View();
        }

        
    }
}
