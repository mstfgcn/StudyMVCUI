using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using WS.MvcUI.ApiServices;
using WS.MvcUI.Areas.AdminPanel.Filters;
using WS.MvcUI.Areas.AdminPanel.Models.ApiTypes;
using WS.MvcUI.Areas.AdminPanel.Models.Dtos;

namespace WS.MvcUI.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class AuthController : Controller
    {

        private readonly IHttpApiService _httpApiService;

        public AuthController(IHttpApiService httpApiService)
        {
            _httpApiService = httpApiService;
        }


        [HttpGet]
        public IActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LogIn(LogInDto dto)
        {
            string endPoint = $"/AdminPanelUser/login?userName={dto.UserName}&password={dto.Password}";
            var response = await _httpApiService.GetData<ResponseBody<AdminPanelUserItem>>(endPoint);



            if (response.StatusCode == 200)
            {
                //value string istedigi için çevirdik.
                //kullanıcı bilgisini(response.Data) sessiona bu key(activeadminpaneluser) ile kaydet
                HttpContext.Session.SetString("ActiveAdminPanelUser", JsonSerializer.Serialize(response.Data));
                await GetTokenAndSetInSession();                
                return Json(new { IsSuccess = true, Message = "başaralı" });
            }
            else
            {
                return Json(new { IsSuccess = false, Message = response.ErrorMessages });
            }


        }

        public async Task GetTokenAndSetInSession()
        {
            var response = await _httpApiService.GetData<ResponseBody<AccesTokenItem>>($"/AdminPanelUser/gettoken");

            HttpContext.Session.SetString("AccesToken", JsonSerializer.Serialize<AccesTokenItem>(response.Data));

        }
    }
}
