using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text.Json;
using WS.MvcUI.Areas.AdminPanel.Models.ApiTypes;

namespace WS.MvcUI.Areas.AdminPanel.Filters
{
    public class SessionControlFilter:ActionFilterAttribute
    {
        //method ilktediklendiginde session kontrolu yapsın

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var sessionData =context.HttpContext.Session.GetString("ActiveAdminPanelUser");


            var admin = JsonSerializer.Deserialize<AdminPanelUserItem>(sessionData);

            if (string.IsNullOrEmpty(sessionData)) 
            {
                context.Result = new RedirectToActionResult("LogIn", "Auth", null);         
            }


        }
    }
}
