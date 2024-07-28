using WS.MvcUI.ApiServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession(); // uygulama i�inde HttpContext.Session � kullanabilmemizi sa�lamak i�in cotainer a register ediyoruz.
builder.Services.AddHttpContextAccessor();


//IoC ye  HttpClient olu�turmak ve HttpApiServici  nesnelerini �retmek i�in gerekli direktif.
builder.Services.AddHttpClient();
builder.Services.AddScoped<IHttpApiService, HttpApiService>();

var app = builder.Build();




// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseSession(); // uygulama i�inde HttpContext.Session � kullanabilmemizi sa�lamak i�in pipeline a bu middleware i ekliyoruz

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


//area lar i�in adres tan�m� yapt�k
app.MapAreaControllerRoute(
    name:"adminPanelDefault",
    areaName:"AdminPanel",
    pattern: "{area}/{controller=Auth}/{action=LogIn}/{id?}"
    );
// http://localhost:5154/adminpanel/Test/index  
app.Run();
