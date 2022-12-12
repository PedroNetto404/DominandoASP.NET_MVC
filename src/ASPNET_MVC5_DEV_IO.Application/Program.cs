using ASP_NET_MVC5_DEV_IO.Infrastructure.Data.Context;
using ASPNET_MVC5_DEV_IO.Application.Configurations;
using ASPNET_MVC5_DEV_IO.Application.Extensions;
using Microsoft.EntityFrameworkCore;

namespace ASPNET_MVC5_DEV_IO.Application
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CultureConfig.RegisterCulture();
            
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);
            builder.Services.AddDbContext<MeuDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
            }); 
            builder.Services.AddDependencyInjection(); 
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
                pattern: "{controller=Fornecedores}/{action=Index}/{id?}");

            app.Run();
        }
    }
}