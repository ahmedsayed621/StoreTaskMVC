using Microsoft.EntityFrameworkCore;
using StoreTaskMVC.Data;
using StoreTaskMVC.Interfaces;
using StoreTaskMVC.Services;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IStoreService, StoreService>();
builder.Services.AddScoped<ISpaceService, SpaceService>();

builder.Services.AddDbContext<StoreDbContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString")));

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
    name: "default",
    pattern: "{controller=Stores}/{action=Index}/{id?}");

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
var loggerFactory = services.GetRequiredService<ILoggerFactory>();

try
{
    var context = services.GetRequiredService<StoreDbContext>();

    context.Database.Migrate();
    StoreDbInitializerExtension.Seed(context, loggerFactory);
}
catch (Exception ex)
{
    var logger = loggerFactory.CreateLogger<ILogger<Program>>();
    logger.LogError(ex, "Error occured while migrating process");
}

app.Run();
