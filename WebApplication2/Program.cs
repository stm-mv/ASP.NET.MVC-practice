using WebApplication2.Domain.Repos.EntityFramework;
using WebApplication2.Domain.Repos.Abstract;
using WebApplication2.Service;
using WebApplication2.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

Config? config = null;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.json");

var connetion = builder.Configuration.GetConnectionString("Default");

config = builder.Configuration.GetRequiredSection("Settings").Get<Config>();

builder.Services.AddTransient<ITextFiedsRepository,EFTextFieldsRepository>();
builder.Services.AddTransient<IServiceItemsRepository,EFServiceItemsRepository>();
builder.Services.AddTransient<DataManager>();

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connetion));

builder.Services.AddIdentity<IdentityUser, IdentityRole>(opts =>
{
    opts.User.RequireUniqueEmail = true;
    opts.Password.RequiredLength = 6;
    opts.Password.RequireNonAlphanumeric = false;
    opts.Password.RequireLowercase = false;
    opts.Password.RequireUppercase = false;
    opts.Password.RequireDigit = false;
}).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();


builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.Name = "myCompanyAuth";
    options.Cookie.HttpOnly = true;
    options.LoginPath = "/account/login";
    options.AccessDeniedPath = "/account/accessdenied";
    options.SlidingExpiration = true;
});

builder.Services.AddAuthorization(x =>
{
    x.AddPolicy("AdminArea", policy => { policy.RequireRole("admin"); });
});


builder.Services.AddControllersWithViews(
    x =>
{
    x.Conventions.Add(new AdminAreaAuthorization("Admin", "AdminArea"));
}
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();
app.UseRouting();
app.UseCookiePolicy();
app.UseAuthentication();
app.UseAuthorization();

//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapControllerRoute("admin", "{controller=Home}/{action=Index}/{id?}");
//    endpoints.MapControllerRoute("default", "{area:exists}/{controller=Home}/{action=Index}/{id?}");
//});


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapAreaControllerRoute(
    name: "admin",
    areaName: "Admin",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.Run();
