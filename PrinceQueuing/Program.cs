using ExternalLogin.Interfaces;
using ExternalLogin.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using PrinceQ.DataAccess.Data.Context;
using PrinceQ.DataAccess.Repository;
using PrinceQ.Models.Entities;
using PrinceQ.DataAccess.Interfaces;
using PrinceQ.DataAccess.Services;
using PrinceQ.DataAccess.Hubs;
using ExternalLogin;
using Serilog;
using Microsoft.AspNetCore.Authorization;
using PrinceQ.Utility;
using Microsoft.AspNetCore.HttpOverrides;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((ctx, lc) => lc.WriteTo.Console().ReadFrom.Configuration(ctx.Configuration));

// Add services to the container.
builder.Services.AddControllersWithViews();

// set the db connection
var CentralLoginConnectionString = builder.Configuration.GetConnectionString("CentralLoginConnection")
?? throw new InvalidOperationException("Connection string 'ExternalDbContext' not found.");

builder.Services.AddDbContext<ExternalDbContext>(options =>
    options.UseSqlServer(CentralLoginConnectionString));

//external login
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IExternalLoginService, ExternalLoginService>();


builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredUniqueChars = 0;
})
    .AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

builder.Services.AddDetection();
// Register the repositories
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IClerk, ClerkService>();
builder.Services.AddScoped<IAdmin, AdminService>();
builder.Services.AddScoped<IRegisterPersonnel, RegisterPersonnelService>();
builder.Services.AddSignalR();

builder.Services.Configure<KestrelServerOptions>(options =>
{
    options.Limits.MaxRequestBodySize = 524288000;
});


//builder.Services.AddAuthorization(options =>
//{
//    options.DefaultPolicy = new AuthorizationPolicyBuilder()
//        .RequireAuthenticatedUser()
//        .Build();


//    options.AddPolicy(SD.Policy_Staff_Admin, policy => policy.RequireAssertion(context =>
//        context.User.IsInRole(SD.Role_Admin)
//        || context.User.IsInRole(SD.Role_Staff1)
//        || context.User.IsInRole(SD.Role_Staff2)
//        || context.User.IsInRole(SD.Role_Staff3)
//        || context.User.IsInRole(SD.Role_Staff4)
//        || context.User.IsInRole(SD.Role_Staff5)
//    ));
    


//});



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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
     pattern: "{controller=Account}/{action=Login}/{id?}");

app.MapHub<QueueHub>("/PrinceQ.DataAccess/hubs/queueHub");

app.Run();
