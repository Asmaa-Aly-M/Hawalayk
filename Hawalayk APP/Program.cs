//using Hawalayk_APP.Context;
//using Hawalayk_APP.Helpers;
//using Hawalayk_APP.Middlewares;
//using Hawalayk_APP.Models;
//using Hawalayk_APP.Services;
//using Hawalayk_APP.System_Hub;
//using Microsoft.AspNetCore.Authentication.JwtBearer;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.IdentityModel.Tokens;
//using System.Text;

//namespace Hawalayk_APP
//{
//    public class Program
//    {
//        public static void Main(string[] args)
//        {

//            var builder = WebApplication.CreateBuilder(args);
//            #region register service 
//            builder.Services.AddSingleton<NotificationHub>();

//            // Add services to the container.
//            builder.Services.AddTransient<BanMiddleware>();
//            builder.Services.AddScoped<IBlockingService, BlockingService>();
//            builder.Services.AddScoped<IBanService, BanService>();
//            builder.Services.AddScoped<ISeedingDataService, SeedingDataService>();
//            builder.Services.AddScoped<IApplicationUserService, ApplicationUserService>();

//            builder.Services.Configure<JWT>(builder.Configuration.GetSection("JWT"));
//            builder.Services.Configure<TwilioSettings>(builder.Configuration.GetSection("Twilio"));
//            builder.Services.AddTransient<ISMSService, SMSService>();

//            builder.Services.AddScoped<ICraftRepository, CraftRepository>();
//            builder.Services.AddScoped<IGovernorateService, GovernorateService>();
//            builder.Services.AddScoped<ICityService, CityService>();
//            builder.Services.AddScoped<IAddressService, AddressService>();
//            builder.Services.AddScoped<ICraftsmenRepository, CraftsmenRepository>();
//            builder.Services.AddScoped<IAdminRepository, AdminRepository>();
//            builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
//            builder.Services.AddScoped<IAuthService, AuthService>();
//            builder.Services.AddScoped<IPostRepository, PostRepository>();
//            builder.Services.AddScoped<IAdvertisementRepository, AdvertisementRepository>();
//            builder.Services.AddScoped<IAppReportRepository, AppReportRepository>();
//            builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
//            builder.Services.AddScoped<IUserReportRepository, UserReportRepository>();
//            builder.Services.AddScoped<IServiceRequestRepository, ServiceRequestRepository>();

//            builder.Services.AddScoped<IJobApplicationRepository, JobApplicationRepository>();

//            #endregion
//            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
//                .AddEntityFrameworkStores<ApplicationDbContext>();

//            builder.Services.AddDbContext<ApplicationDbContext>(options =>
//                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));//DefaultConnection

//            //builder.Services.AddAuthentication(options =>
//            //{
//            //    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//            //    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationSchemehttp://hnewversion.runasp.net/swagger/index.html;
//            //})

//            //.AddJwtBearer(options =>
//            //{
//            //    options.RequireHttpsMetadata = false;
//            //    options.SaveToken = true;
//            //    options.TokenValidationParameters = new TokenValidationParameters
//            //    {
//            //        ValidateIssuer = true,
//            //        ValidateAudience = true,
//            //        ValidateLifetime = true,
//            //        ValidateIssuerSigningKey = true,
//            //        ValidIssuer = builder.Configuration["JWT:Issuer"],
//            //        ValidAudience = builder.Configuration["JWT:Audience"],
//            //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))
//            //    };
//            //});

//            builder.Services.AddAuthentication(options =>
//            {
//                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//            })
//            .AddJwtBearer(options =>
//            {
//                options.TokenValidationParameters = new TokenValidationParameters
//                {
//                    ValidateIssuer = true,
//                    ValidateAudience = true,
//                    ValidateLifetime = true,
//                    ValidateIssuerSigningKey = true,
//                    ValidIssuer = builder.Configuration["JWT:Issuer"],
//                    ValidAudience = builder.Configuration["JWT:Audience"],
//                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))
//                };

//                options.Events = new JwtBearerEvents
//                {
//                    OnMessageReceived = context =>
//                    {
//                        var accessToken = context.Request.Query["access_token"];

//                        // If the request is for the hub...
//                        var path = context.HttpContext.Request.Path;
//                        if (!string.IsNullOrEmpty(accessToken) &&
//                            (path.StartsWithSegments("/notificationHub")))
//                        {
//                            context.Token = accessToken;
//                        }
//                        return Task.CompletedTask;
//                    }
//                };
//            });


//            builder.Services.AddCors(options =>
//            {
//                options.AddPolicy("AllowReact",
//                  builder =>
//                  {
//                      builder
//                      .WithOrigins("http://localhost:3000")
//                      .AllowAnyMethod()
//                      .AllowAnyHeader()
//                      .SetIsOriginAllowedToAllowWildcardSubdomains()  // Allow subdomains of localhost
//                      .AllowCredentials();  // This is the key change
//                  });
//            });

//            builder.Services.AddSignalR();
//            //options =>
//            //    {
//            //        options.EnableDetailedErrors = true; // Optional for debugging
//            //    });



//            builder.Services.AddControllers();
//            builder.Services.AddEndpointsApiExplorer();


//            builder.Services.AddSwaggerGen();


//            builder.Services.AddSwaggerGen(c =>
//            {
//                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Your API", Version = "v1" });
//                c.OperationFilter<SecurityRequirementsOperationFilter>();
//                c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
//                {
//                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\"",
//                    Name = "Authorization",
//                    In = Microsoft.OpenApi.Models.ParameterLocation.Header,
//                    Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
//                    Scheme = "Bearer"
//                });
//            });

//            #region seeding Address
//            // Register DataSeeder with the DI container
//            builder.Services.AddScoped<DataSeeder>();

//            // Build the service provider
//            using var serviceProvider = builder.Services.BuildServiceProvider();

//            // Resolve DataSeeder from the service provider
//            var seeder = serviceProvider.GetRequiredService<DataSeeder>();

//            // Call the seeding methods
//            seeder.SeedGovernoratesData();
//            seeder.SeedCitiesData();
//            //start time min : det  : banned : > var : det - s
//            #endregion


//            var app = builder.Build();

//            //using (var scope = app.Services.CreateScope())
//            //{
//            //    var services = scope.ServiceProvider;
//            //    var dbContext = services.GetRequiredService<ApplicationDbContext>();
//            //    dbContext.Database.Migrate();

//            //    var seedService = services.GetRequiredService<ISeedingDataService>();
//            //    seedService.SeedingData();
//            //}

//            app.UseSwagger();
//            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API V1"));
//            app.UseStaticFiles();
//            app.UseCors("AllowReact");

//            app.UseAuthentication();

//            app.UseAuthorization();

//            app.UseMiddleware<BanMiddleware>();

//            app.MapControllers();




//            app.MapHub<NotificationHub>("/notificationHub"); // Map the hub to a URL endpoint


//            app.Run();
//        }
//    }


//    public class SecurityRequirementsOperationFilter : Swashbuckle.AspNetCore.SwaggerGen.IOperationFilter
//    {
//        public void Apply(Microsoft.OpenApi.Models.OpenApiOperation operation, Swashbuckle.AspNetCore.SwaggerGen.OperationFilterContext context)
//        {
//            if (operation.Security == null)
//                operation.Security = new System.Collections.Generic.List<Microsoft.OpenApi.Models.OpenApiSecurityRequirement>();

//            var scheme = new Microsoft.OpenApi.Models.OpenApiSecurityScheme
//            {
//                Reference = new Microsoft.OpenApi.Models.OpenApiReference
//                {
//                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
//                    Id = "Bearer"
//                }
//            };
//            // cod e : url : hub
//            operation.Security.Add(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
//            {
//                [scheme] = new System.Collections.Generic.List<string>()
//            });
//        }
//    }
//}
using Hawalayk_APP.Context;
using Hawalayk_APP.Helpers;
using Hawalayk_APP.Middlewares;
using Hawalayk_APP.Models;
using Hawalayk_APP.Services;
using Hawalayk_APP.System_Hub;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Hawalayk_APP
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container
            builder.Services.AddScoped<NotificationHub>();
            builder.Services.AddTransient<BanMiddleware>();
            builder.Services.AddScoped<IBlockingService, BlockingService>();
            builder.Services.AddScoped<IBanService, BanService>();
            builder.Services.AddScoped<ISeedingDataService, SeedingDataService>();
            builder.Services.AddScoped<IApplicationUserService, ApplicationUserService>();
            builder.Services.Configure<JWT>(builder.Configuration.GetSection("JWT"));
            builder.Services.Configure<TwilioSettings>(builder.Configuration.GetSection("Twilio"));
            builder.Services.AddTransient<ISMSService, SMSService>();
            builder.Services.AddScoped<ICraftRepository, CraftRepository>();
            builder.Services.AddScoped<IGovernorateService, GovernorateService>();
            builder.Services.AddScoped<ICityService, CityService>();
            builder.Services.AddScoped<IAddressService, AddressService>();
            builder.Services.AddScoped<ICraftsmenRepository, CraftsmenRepository>();
            builder.Services.AddScoped<IAdminRepository, AdminRepository>();
            builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<IPostRepository, PostRepository>();
            builder.Services.AddScoped<IAdvertisementRepository, AdvertisementRepository>();
            builder.Services.AddScoped<IAppReportRepository, AppReportRepository>();
            builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
            builder.Services.AddScoped<IAddressService, AddressService>();
            builder.Services.AddScoped<IUserReportRepository, UserReportRepository>();
            builder.Services.AddScoped<IServiceRequestRepository, ServiceRequestRepository>();
            builder.Services.AddScoped<IJobApplicationRepository, JobApplicationRepository>();

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));//serverserver

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["JWT:Issuer"],
                    ValidAudience = builder.Configuration["JWT:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))
                };

                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];
                        var path = context.HttpContext.Request.Path;
                        if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/notificationHub"))
                        {
                            context.Token = accessToken;
                        }
                        return Task.CompletedTask;
                    }
                };
            });

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                {
                    builder
                    .SetIsOriginAllowed(origin => true) // Allow any origin
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();
                });
            });


            builder.Services.AddSignalR();
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Your API", Version = "v1" });
                c.OperationFilter<SecurityRequirementsOperationFilter>();
                c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\"",
                    Name = "Authorization",
                    In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                    Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
            });


            //#region seeding Address
            //// Register DataSeeder with the DI container
            //builder.Services.AddScoped<DataSeeder>();

            //// Build the service provider
            //using var serviceProvider = builder.Services.BuildServiceProvider();

            //// Resolve DataSeeder from the service provider
            //var seeder = serviceProvider.GetRequiredService<DataSeeder>();

            //// Call the seeding methods
            //seeder.SeedGovernoratesData();
            //seeder.SeedCitiesData();
            ////start time min : det  : banned : > var : det - s
            //#endregion

            var app = builder.Build();

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API V1"));
            app.UseStaticFiles();
            app.UseCors("AllowAll");
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseMiddleware<BanMiddleware>();
            app.MapControllers();
            app.MapHub<NotificationHub>("/notificationHub");
            app.MapHub<CallHub>("/callHub");
            app.Run();
        }
    }

    public class SecurityRequirementsOperationFilter : Swashbuckle.AspNetCore.SwaggerGen.IOperationFilter
    {
        public void Apply(Microsoft.OpenApi.Models.OpenApiOperation operation, Swashbuckle.AspNetCore.SwaggerGen.OperationFilterContext context)
        {
            if (operation.Security == null)
                operation.Security = new System.Collections.Generic.List<Microsoft.OpenApi.Models.OpenApiSecurityRequirement>();

            var scheme = new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            };

            operation.Security.Add(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
            {
                [scheme] = new System.Collections.Generic.List<string>()
            });
        }
    }
}
