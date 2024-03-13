using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoRepairService.Core.IServices;
using AutoRepairService.Core.Services;
using AutoRepairService.Hubs;
using AutoRepairService.Infrastructure.Data.Common;
using AutoRepairService.Infrastructure.Data.EntityModels;
using AutoRepairService.Infrastructure.Data;
using AutoRepairService.ModelBinders;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<User>(options => {
    options.SignIn.RequireConfirmedAccount = false;
    options.SignIn.RequireConfirmedEmail = false;
    options.Password.RequireDigit = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 4;
})
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/User/Login";
    options.LogoutPath = "/User/Logout";
});

builder.Services.AddScoped<IRepository, Repository>();
builder.Services.AddScoped<IRepairService, RepairService>();
builder.Services.AddScoped<IOfferService, OfferService>();
builder.Services.AddScoped<ICarService, CarService>();
builder.Services.AddScoped<IAdminCarService, AdminCarService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<IRepairAdministrationService, RepairAdministrationService>();
builder.Services.AddScoped<IMechanicService, MechanicService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<StatisticAdministrationService>();

builder.Services.AddControllersWithViews().AddMvcOptions(options =>
{
    options.Filters.Add<AutoValidateAntiforgeryTokenAttribute>();
    options.ModelBinderProviders.Insert(0, new DecimalModelBinderProvider());
});

builder.Services.AddResponseCaching();
builder.Services.AddSignalR();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/Home/Error", "?statusCode={0}");
app.UseExceptionHandler("/Home/Error");

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );

    endpoints.MapControllerRoute(
      name: "default",
      pattern: "{controller=Home}/{action=Index}/{id?}"
        );
});

app.MapRazorPages();
app.MapHub<LiveChatHub>("/chat");

app.Run();