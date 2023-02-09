using DataAccess;
using Logic;
using Microsoft.EntityFrameworkCore;
using Models;

var builder = WebApplication.CreateBuilder(args);

// GET Enviromental Variables from appsettings.json
builder.Configuration.AddJsonFile("appsettings.json", true, true);

// Configuring Database

#region Connection to Database

var connectionString = builder.Configuration.GetConnectionString("LOCAL_DB");
builder.Services.AddDbContext<DBContext>(options =>
    options.UseSqlServer(connectionString));

#endregion

// Add services to the container.
builder.Services.AddControllersWithViews();

#region Dependency Injection

builder.Services.AddScoped<CategoriaAccess>();
builder.Services.AddScoped<ProductoAccess>();
builder.Services.AddScoped<CoreLogic>();

#endregion

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

app.UseAuthorization();

app.MapControllerRoute(
    "default",
    "{controller=Home}/{action=Index}/{id?}");

app.Run();