using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using Talabat.APIs.Errors;
using Talabat.APIs.Extensions;
using Talabat.APIs.Helpers;
using Talabat.APIs.MiddleWare;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Repositories.Contract;
using Talabat.Repository;
using Talabat.Repository.Data;
using Talabat.Repository.Identity;
using Talabat.Repository.Identity.Dataseed;

namespace Talabat.APIs
{
    public class Program
    {
        public static async Task Main (string[] args)
        {
            var webApplicationbuilder = WebApplication.CreateBuilder(args);

            // Add services to the DI container.

            #region Configure Services
            //Register Required with APIs Sevices to the DI Container
            webApplicationbuilder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

            webApplicationbuilder.Services.AddSwaggerServices();

            webApplicationbuilder.Services.AddDbContext<StoreContext>(options=>options.UseSqlServer(webApplicationbuilder.Configuration.GetConnectionString("DefaultConnection")));

            webApplicationbuilder.Services.AddDbContext<AppIdentityDbContext>(options => options.UseSqlServer(webApplicationbuilder.Configuration.GetConnectionString("Identity")));



            webApplicationbuilder.Services.AddSingleton<IConnectionMultiplexer>(
                servicesProvider =>
                {
                    var Connection = webApplicationbuilder.Configuration.GetConnectionString("Redis");
                    return ConnectionMultiplexer.Connect(Connection);
                }
                );
            webApplicationbuilder.Services.AddApplicationServices();

            webApplicationbuilder.Services.AddIdentity<AppUser, IdentityRole>( options => {

            }).AddEntityFrameworkStores<AppIdentityDbContext>();
            #endregion
            //Configure 6 things --> which include Services (De mn dmnohom)



            var app = webApplicationbuilder.Build();

           using var Scope = app.Services.CreateScope();

            var Services = Scope.ServiceProvider;
           

            var _dbContext= Services.GetRequiredService<StoreContext>(); //Ask CLR to create object from DbContext Explicitly
            var _IdentityContext = Services.GetRequiredService<AppIdentityDbContext>();
            var Logger_Factory = Services.GetRequiredService<ILoggerFactory>();
            var Logger = Logger_Factory.CreateLogger<Program>();

            try
            {
                await _dbContext.Database.MigrateAsync();
                await StoreContextseed.SeedAsync(_dbContext);


                await _IdentityContext.Database.MigrateAsync();

                var _userManager = Services.GetRequiredService<UserManager<AppUser>>();
                await AppIdentityDbContextSeed.SeedUserAsync(_userManager);



            }
            catch(Exception ex)
            {
             
            
                Logger.LogError(ex, "an error has been occured in Migration");


            }

            #region Configure Kestrel Middlewares

            app.UseMiddleware<ExceptionMiddleware>();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwaggerServices();
            }

            app.UseStatusCodePagesWithRedirects("/errors/{0}");
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthorization();
         

            app.MapControllers(); 
            #endregion

            app.Run();
        }
    }
}