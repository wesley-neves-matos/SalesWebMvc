﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore;
using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Data;
using SalesWebMvc;
using SalesWebMvc.Services;


internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        string connectionString = builder.Configuration.GetConnectionString("SalesWebMvcContext");
        builder.Services.AddDbContext<SalesWebMvcContext>(options =>
                     options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

        // Add services to the container.
        builder.Services.AddControllersWithViews();

        builder.Services.AddScoped<SeedingService>();
        builder.Services.AddScoped<SellerService>();

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

            using (var scope = app.Services.CreateScope())
            {
                var seedingService = scope.ServiceProvider.GetRequiredService<SeedingService>();
                seedingService.Seed();
            }
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }



}