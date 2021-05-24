using AspNet.Security.OpenIdConnect.Primitives;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Northstar.WS.Filters;
using Northstar.WS.Models;
using Northstar.WS.Models.DTO;
using Northstar.WS.Services;
using Northstar.WS.Services.ControllerServices;
using Northstar.WS.Utility;
using System;
using OpenIddict.Validation;

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
            ).AddJsonOptions(
                options =>
                {
                    options.JsonSerializerOptions.IgnoreNullValues = true;
                }
            );

            //not a singleton service. Will be created everytime the room controller is invoked
            services.AddScoped<IRoomService, RoomService>();
            services.AddScoped<IHotelService, HotelService>();
            services.AddScoped<IUserService, UserService>();

            services.AddDbContext<AvimoreDBContext>(
                options =>
                {
                    options.UseSqlServer(CommonConstants.DefaultConnectionStringAvimoreDb);
                    options.UseOpenIddict<Guid>();
                });

            services.AddOpenIddict()
                .AddCore(options =>
                {
                    options.UseEntityFrameworkCore()
                    .UseDbContext<AvimoreDBContext>()
                    .ReplaceDefaultEntities<Guid>();
                })
                .AddServer(options =>
                {
                    options.UseMvc();
                    options.SetTokenEndpointUris("/api/token");
                    options.AllowPasswordFlow();
                    options.AcceptAnonymousClients();
                })
                .AddValidation();

            services.Configure<IdentityOptions>(
                options =>
                {
                    options.ClaimsIdentity.UserNameClaimType = OpenIdConnectConstants.Claims.Name;
                    options.ClaimsIdentity.UserNameClaimType = OpenIdConnectConstants.Claims.Subject;
                    options.ClaimsIdentity.RoleClaimType = OpenIdConnectConstants.Claims.Role;
                });

            //IMPORTANT TO ADD if IdentityCoreServices are being added
            services.AddAuthentication();
            /*services.AddAuthentication(options =>
            {
                options.DefaultScheme = OpenIddict.Validation.valid
            });*/

            AddIdentityCoreServices(services);


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

            app.UseAuthentication();

            app.UseCors("AllowMyApp");

            app.UseRouting();

            app.UseAuthorization();

            app.UseResponseCaching();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private static void AddIdentityCoreServices(IServiceCollection services)
        {
            var builder = services.AddIdentityCore<UserDTO>();
            builder = new IdentityBuilder(builder.UserType,
                typeof(UserRoleDTO),
                builder.Services);
            builder.AddRoles<UserRoleDTO>()
                .AddEntityFrameworkStores<AvimoreDBContext>()
                .AddDefaultTokenProviders()
                .AddSignInManager<SignInManager<UserDTO>>();
        }
    }
}
