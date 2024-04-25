using Hawalayk_APP.Context;
using Hawalayk_APP.Helpers;
using Hawalayk_APP.Models;
using Hawalayk_APP.Services;
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

            // Add services to the container.
            #region Register the services in Program.cs file:
            builder.Services.AddScoped<ISeedingDataService, SeedingDataService>();
            builder.Services.AddScoped<IApplicationUserService, ApplicationUserService>();

            builder.Services.Configure<JWT>(builder.Configuration.GetSection("JWT"));

            builder.Services.Configure<TwilioSettings>(builder.Configuration.GetSection("Twilio"));


            builder.Services.AddTransient<ISMSService, SMSService>();


            builder.Services.AddScoped<ICraftRepository, CraftRepository>();
            builder.Services.AddScoped<ICraftsmenRepository, CraftsmenRepository>();
            builder.Services.AddScoped<IAdminRepository, AdminRepository>();
            builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<IPostRepository, PostRepository>();
            builder.Services.AddScoped<IAdvertisementRepository, AdvertisementRepository>();
            builder.Services.AddScoped<IAppReportRepository, AppReportRepository>();
            builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
            builder.Services.AddScoped<IUserReportRepository, UserReportRepository>();
            builder.Services.AddScoped<IServiceRequestRepository, ServiceRequestRepository>();

            builder.Services.AddScoped<IBlockingService, BlockingService>();
            builder.Services.AddScoped<ISeedingDataService, SeedingDataService>();
            #endregion

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
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
            });

            builder.Services.AddCors();
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

            var app = builder.Build();

            #region Inject and use the service in the Program.cs file:

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var dbContext = services.GetRequiredService<ApplicationDbContext>();
                dbContext.Database.Migrate();

                var seedService = services.GetRequiredService<ISeedingDataService>();
                seedService.SeedingData();
            }
            #endregion

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API V1"));
            }

            app.UseCors(c => c.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

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
