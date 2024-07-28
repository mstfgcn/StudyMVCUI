using WS.MvcUI.ApiServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession(); // uygulama içinde HttpContext.Session ý kullanabilmemizi saðlamak için cotainer a register ediyoruz.
builder.Services.AddHttpContextAccessor();


//IoC ye  HttpClient oluþturmak ve HttpApiServici  nesnelerini üretmek için gerekli direktif.
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

app.UseSession(); // uygulama içinde HttpContext.Session ý kullanabilmemizi saðlamak için pipeline a bu middleware i ekliyoruz

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


//area lar için adres tanýmý yaptýk
app.MapAreaControllerRoute(
    name:"adminPanelDefault",
    areaName:"AdminPanel",
    pattern: "{area}/{controller=Auth}/{action=LogIn}/{id?}"
    );
// http://localhost:5154/adminpanel/Test/index  
app.Run();
