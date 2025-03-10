using System.Text;
using Application.Repositories;
using Application.Services.Address;
using Application.Services.AddressUser;
using Application.Services.Cars;
using Application.Services.OfferCarpooling;
using Application.Services.Profile;
using Application.Services.RequestCarpooling;
using Application.Services.UserProfile;
using Application.Services.Users;
using Infrastructure.SqlServer.Address;
using Infrastructure.SqlServer.AddressUser;
using Infrastructure.SqlServer.Cars;
using Infrastructure.SqlServer.OfferCarpooling;
using Infrastructure.SqlServer.Profile;
using Infrastructure.SqlServer.RequestCarpooling;
using Infrastructure.SqlServer.UserProfile;
using Infrastructure.SqlServer.Users;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using pAPI.DashboardFeatures;
using pAPI.Extensions;
using pAPI.Hubs;

namespace pAPI
{
    public class Startup
    {
        public static readonly string MyOrigins = "MyOrigins";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Uniquement en développement !!!!
            services.AddCors(options =>
            {

                options.AddPolicy(MyOrigins, builder =>
                {
                    builder.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader().AllowCredentials();
                    builder.WithOrigins("http://localhost:6205").AllowAnyMethod().AllowAnyHeader().AllowCredentials();
                    builder.WithOrigins("https://infallible-spence-01c804.netlify.app").AllowAnyMethod()
                        .AllowAnyHeader().AllowCredentials();
                });
            });


            services.AddControllers();

            services.AddSignalR();

            services.AddHostedService<DashboardHostedService>();

            services.AddSingleton<IUserService, UserService>();
            services.AddSingleton<IUserRepository, UserRepository>();
            
            services.AddSingleton<ICarService, CarService>();
            services.AddSingleton<ICarRepository, CarRepository>();
            
            services.AddSingleton<IAddressService, AddressService>();
            services.AddSingleton<IAddressRepository, AddressRepository>();
            
            services.AddSingleton<IProfileService, ProfileService>();
            services.AddSingleton<IProfileRepository, ProfileRepository>();

            services.AddSingleton<IAddressUserService, AddressUserService>();
            services.AddSingleton<IAddressUserRepository, AddressUserRepository>();
            
            services.AddSingleton<IUserProfileService, UserProfileService>();
            services.AddSingleton<IUserProfileRepository, UserProfileRepository>();
            
            services.AddSingleton<IOfferCarpoolingService, OfferCarpoolingService>();
            services.AddSingleton<IOfferCarpoolingRepository, OfferCarpoolingRepository>();
            
            services.AddSingleton<IRequestCarpoolingRepository, RequestCarpoolingRepository>();
            services.AddSingleton<IRequestCarpoolingService, RequestCarpoolingService>();
            
            // configure strongly typed settings objects
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            // configure jwt authentication
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            if (env.IsProduction())
            {
                app.UseHttpsRedirection();
            }

            app.UseCors(MyOrigins);

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<NotificationHub>("/notifications"); 
            });
        }
    }
}