using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using WS.MvcUI.Areas.AdminPanel.Models.ApiTypes;


namespace WS.MvcUI.Areas.AdminPanel.ViewComponents
{
    public class SideBarViewComponent : ViewComponent
    {
        //bu componen içindeki metodun adı Invoke olmak zorunda
        public IViewComponentResult Invoke()
        {

            var sessionData = HttpContext.Session.GetString("ActiveAdminPanelUser");
            

            var admin = JsonSerializer.Deserialize<AdminPanelUserItem>(sessionData);

            return View(admin);
        }

    }
}
