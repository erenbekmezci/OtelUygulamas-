using DataAccess.Abstract;
using DataAccess.Concrete.EfCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileSystemGlobbing.Internal.Patterns;
using teknofest.Identity;
using Microsoft.AspNetCore.Hosting;
using System.Configuration;
using ShopApp.webui.EmailServices;
using Business.Abstract;
using Business.Concrete;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<ApplicationContext>(options => options.UseNpgsql("Host=localhost;Database=OtelApp;Username=postgres;Password=kardelen67"));
builder.Services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<ApplicationContext>().AddDefaultTokenProviders(); //resetlemede benzersiz bir token verir


IConfiguration Configuration = new ConfigurationBuilder()
                            .AddJsonFile("appsettings.json")
                            .Build();






//builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
//    .AddEntityFrameworkStores<ApplicationDbContext>();  default identity


builder.Services.Configure<IdentityOptions>(options =>
{
    // Password settings.
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true; //_@* vs
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;


    // Lockout settings. //hesabýn kilitlenmesi olaylarý
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5; //defa yanlýþ girdikten sonra 5 dk hesap kitlenir 
    options.Lockout.AllowedForNewUsers = true; 

    // User settings.
    //options.User.AllowedUserNameCharacters =
    //"abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = true;
    options.SignIn.RequireConfirmedEmail = false; // üyelik onaylanmasý projenin ilk aþamalarý için(web ödevi için önemli deðil teknofest için true olucak)
    options.SignIn.RequireConfirmedPhoneNumber = false;
});

builder.Services.ConfigureApplicationCookie(options =>
{
    // Cookie settings
    options.LoginPath = "/account/login"; //sessioný tanýma yetkigerektiðinde gidilecek alan
    options.LogoutPath = "/account/logout"; //sessiondan çýkma
    options.AccessDeniedPath = "/account/accessdenied";
    options.SlidingExpiration = true; //sessin def olarak 20 dk bunu true yaparsak her istekte 20 dk tekrar baþlar
    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
    options.Cookie.Name = "OtelApp.cookie";

    
    
});


builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IFoodDal, EfCoreFoodDal>();
builder.Services.AddScoped<IFoodCategoryDal , EfCoreFoodCategoryDal>();
builder.Services.AddScoped<ICartDal , EfCoreCartDal>();


builder.Services.AddScoped<ICartService , CartManager>();

builder.Services.AddScoped<IEmailSender, SmtpEmailSender>(i=>
    new SmtpEmailSender(Configuration["EmailSender:Host"],
                Configuration.GetValue<int>("EmailSender:Port"),
                Configuration.GetValue<bool>("EmailSender:EnableSSL"),
                Configuration["EmailSender:UserName"],
                Configuration["EmailSender:Password"])
);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
    SeedDatabase.Seed();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication(); //identity midile ware
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
   endpoints.MapControllerRoute(
       name : "foodcategorydetails",
       pattern : "{url}",
       defaults : new {Controller = "Menu" , Action = "Details" } 
       );


    
   

    endpoints.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
});



app.Run();
