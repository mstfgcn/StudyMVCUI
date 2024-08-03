using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using System.Text.Json;
using WS.MvcUI.ApiServices;
using WS.MvcUI.Areas.AdminPanel.Filters;
using WS.MvcUI.Areas.AdminPanel.Models.ApiTypes;
using WS.MvcUI.Areas.AdminPanel.Models.Dtos.Category;

namespace WS.MvcUI.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    [SessionControlFilter]
    public class CategoryController : Controller
    {
        private readonly IHttpApiService _httpApiService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CategoryController(IHttpApiService httpApiService, IWebHostEnvironment webHostEnvironment)
        {
            _httpApiService = httpApiService;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            var token = HttpContext.Session.GetString("AccesToken");
            var jwt =JsonSerializer.Deserialize<AccesTokenItem>(token);

            //aşagıdaki endpointbir token istiyor. GetData metodu üzerinden bu tokeni endpointe göndermemiz lazım
            var response = await _httpApiService.GetData<ResponseBody<List<CategoryItem>>>("/Categories", jwt.Token);
            

            return View(response.Data);
        }

        public async Task<IActionResult> Save(NewCategoryDto dto, IFormFile categoryImage)
        {
            if (!categoryImage.ContentType.StartsWith("image/"))
                return Json(new { IsSuccess = false, Message = "Sadece resim dosyası geçerlidir." });

            if (categoryImage.Length > 1024 * 250)
                return Json(new { IsSucces = false, Message = "Dosya büyüklügü 250KB dan fazla olamaz." });

            //sunucuya veriyi gönder

            //DOSYA YÜKLEME İŞLEMİ
            var randomFileName = $"{Guid.NewGuid()}{Path.GetExtension(categoryImage.FileName)}";
            var uploadPath = $@"{_webHostEnvironment.WebRootPath}/adminPanel/categoryImages/{randomFileName}";
            //Clienttan gelen veriyi buraya upload edicez.
            using(var fs =new FileStream(uploadPath, FileMode.Create))
            {
                await categoryImage.CopyToAsync(fs);
            }

            var postData = new
            {
                categoryName = dto.CategoryName,
                description = dto.Description,
                picturePath = $@"/adminPanel/categoryImages/{randomFileName}",
                base64Picture = Convert.ToBase64String(System.IO.File.ReadAllBytes(uploadPath))
            };

            var response = await _httpApiService.PostData<ResponseBody<CategoryItem>>("/categories", JsonSerializer.Serialize(postData));
           if(response.StatusCode==201)
            return Json(new {IsSucces=true,Message="işlem başarılı"});
            
            var errorMessage = string.Empty;
            foreach (var item in response.ErrorMessages)
            {
                errorMessage += item + "<br/>";
            }

            return Json(new { IsSucces = false, Message = $"işlem başarısız.<br/> {errorMessage}" });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpApiService.Delete($@"/categories/{id}");
            if (response)
                return Json(new { IsSucces = true, Message = "Ketegori Silindi" });

            return Json(new { IsSucces = false, Message = "Kategori Silinemedi." });
        }

        [HttpPost]
        public async Task<IActionResult> GetCategory(int id)
        {

            var response = await _httpApiService.GetData<ResponseBody<CategoryItem>>($@"/Categories/{id}");


            return Json(new {
            CategoryName =response.Data.CategoryName,
            Description=response.Data.Description,
            PicturePath = response.Data.PicturePath,
            CategoryId = response.Data.CategoryId,
            });
        }

    }
}
