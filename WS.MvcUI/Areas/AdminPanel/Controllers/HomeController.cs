using Microsoft.AspNetCore.Mvc;
using WS.MvcUI.Areas.AdminPanel.Filters;

namespace WS.MvcUI.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class HomeController : Controller
    {
        [SessionControlFilter]
        public IActionResult Index()
        {
            return View();
           
        }
    }
}
