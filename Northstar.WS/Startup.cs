using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Northstar.WS.Filters;
using Northstar.WS.Models;
using Northstar.WS.Services;
using Northstar.WS.Utility;

namespace Northstar.WS
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(
                options =>
                {
                    options.CacheProfiles.Add("Static", new CacheProfile
                    {
                        Duration = 86400
                    });
                    options.Filters.Add<JsonExceptionFilter>();
                    options.Filters.Add<RequireHttpsOrCloseFilter>();
                }
            );

            //not a singleton service. Will be created everytime the room controller is invoked
            services.AddScoped<IRoomService, RoomService>();
            services.AddScoped<IHotelService, HotelService>();

            services.AddDbContext<AvimoreDBContext>(
        options => options.UseSqlServer(CommonConstants.DefaultConnectionStringAvimoreDb));

            services.AddRouting(options => options.LowercaseUrls = true);
            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.ApiVersionReader = new MediaTypeApiVersionReader(); //where to find version information
                options.AssumeDefaultVersionWhenUnspecified = true; //when version not specified, assume default
                options.ReportApiVersions = true; //to get API version info on the responses
                options.ApiVersionSelector = new CurrentImplementationApiVersionSelector(options);
            });
            services.AddCors(options =>
            {
                //options.AddPolicy("AllowMyApp", policy => policy.WithOrigins("https://example.com"));
                options.AddPolicy("AllowMyApp", policy => policy.AllowAnyOrigin()); //allows any origin, recommended ONLY during prouction
            });

            services.AddResponseCaching();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            else
            {
                app.UseHsts();
            }
            //app.UseHttpsRedirection();

            app.UseCors("AllowMyApp");

            app.UseRouting();

            app.UseAuthorization();

            app.UseResponseCaching();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
