using Microsoft.AspNetCore.Mvc;
using WS.MvcUI.Areas.AdminPanel.Filters;

namespace WS.MvcUI.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    [SessionControlFilter]
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return View();

        }
    }
}
