using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete.EntityFramework;
using DataAccessLayer.Context;
using E_Shop.Models;
using E_Shop.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


////context nesnesi ekliyoruz.-AppDbContext-
//builder.Services.AddDbContext<AppDbContext>(options =>
//{//Configuration sýnýfý appsettings.json içindeki datalarý okumamýza imkan verir.
//    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlCon"));
//});


//builder.Services.AddDbContext<AppProContext>(options =>
//{//Configuration sýnýfý appsettings.json içindeki datalarý okumamýza imkan verir.
//    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlCon"));
//});


//builder.Services.AddDbContext<CustomIdentityDbContext>(options =>
//{//Configuration sýnýfý appsettings.json içindeki datalarý okumamýza imkan verir.
//    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlCon"));
//});

//builder.Services.AddIdentity<CustomIdentityUser, CustomIdentityRole>().AddEntityFrameworkStores<CustomIdentityDbContext>().AddDefaultTokenProviders();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());//çalýþmýþ olduðum assembly i al.

builder.Services.AddSingleton<IFileProvider>(new PhysicalFileProvider(Directory.GetCurrentDirectory()));


//senden IProductService örneði istenirse ProductManager nesnesi ver.
builder.Services.AddScoped<IProductService, ProductManager>();
builder.Services.AddScoped<IProductDal, EfProductDal>();

builder.Services.AddScoped<ICategoryService, CategoryManager>();
builder.Services.AddScoped<ICategoryDal,EfCategoryDal>();

builder.Services.AddScoped<ICartSessionService,CartSessionService>();
builder.Services.AddScoped<ICartService,CartService>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddScoped<AppDbContext>();//ÖNEMLÝ:Problem ekleyince çözüldü.
builder.Services.AddSession();//session için

builder.Services.AddMvc();

//builder.Services.AddAuthentication(o =>
//{
//    o.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
//}).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, o => { o.LoginPath = new PathString("/Account/Login/"); });

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
{
    options.Cookie.Name = "CoreMvc.Auth";    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/Login";
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();
app.UseAuthentication();//kullanýcýyý kontrol eder
app.UseAuthorization();//yetkilerini kontrol eder
//app.UseIdentity();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home2}/{action=Index}/{id?}");

app.Run();
