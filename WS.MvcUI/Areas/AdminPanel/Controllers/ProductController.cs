using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using System.Drawing.Text;
using System.Text.Json;
using WS.MvcUI.ApiServices;
using WS.MvcUI.Areas.AdminPanel.Filters;
using WS.MvcUI.Areas.AdminPanel.Models.ApiTypes;
using WS.MvcUI.Areas.AdminPanel.Models.Dtos.Product;

namespace WS.MvcUI.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    [SessionControlFilter]
    public class ProductController : Controller
    {
        private readonly IHttpApiService _httpApiService;

        public ProductController(IHttpApiService httpApiService)
        {
            _httpApiService = httpApiService;
        }
        public async Task<IActionResult> Index()
        {

            var response = await _httpApiService.GetData<ResponseBody<List<ProductItem>>>("/Products");


            return View(response.Data);

        }

        public async Task<IActionResult> Save(NewProductDto dto)
        {
            //gelen degerleri kontrol edebiliriz. adı stogu fiyatı uygunmu ona göre hata döndürebiliriz.

            var postData = new
            {
                ProductName=dto.ProductName,
                UnitStock=dto.UnitStock,
                UnitPrice=dto.UnitPrice,
                CategoryId=dto.CategoryId
            };

             var response = await _httpApiService.PostData<ResponseBody<ProductItem>>("Products", JsonSerializer.Serialize(postData));
            if (response.StatusCode == 201)
                return Json(new { IsSucces = true, Message = "Başarılı" });


            return Json(new { IsSucces = false, Message =response.ErrorMessages });


        }
    }
}