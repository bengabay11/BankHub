using BL;
using Dal;
using BL.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Dal.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace WebAPI
{
    public class AppBuilder(string[] args)
    {
        private readonly WebApplicationBuilder _builder = WebApplication.CreateBuilder(args);

        public WebApplication Build()
        {
            ConfigureServices();

            var app = _builder.Build();
            ConfigureApp(app);
            return app;
        }

        private void ConfigureServices()
        {
            _builder.Services.AddDbContext<BankDbContext>(options =>
                options.UseNpgsql(_builder.Configuration.GetConnectionString("DefaultConnection")));

            _builder.Services.AddScoped<IBankRepository, BankDBRepository>();
            _builder.Services.AddScoped<BankBase>(serviceProvider =>
            {
                return new Bank(
                    _builder.Configuration["Bank:Name"],
                    _builder.Configuration["Bank:Address"],
                    serviceProvider.GetService<IBankRepository>()
                );
            });

            ConfigureAuth();

            // _builder.Services.AddControllers();
            _builder.Services.AddControllers();  // or AddMvc() for broader support of controllers and views
            _builder.Services.AddEndpointsApiExplorer();
            ConfigureSwagger();
        }

        private void ConfigureApp(WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapSwagger().RequireAuthorization();
            app.MapControllers();
            app.MapIdentityApi<User>();
        }

        private void ConfigureAuth()
        {
            // Working!
            // _builder.Services.AddIdentityApiEndpoints<User>()
            //     .AddEntityFrameworkStores<BankDbContext>()
            //     .AddApiEndpoints();
            // _builder.Services.AddTransient<IRoleStore<IdentityRole<Guid>>, RoleStore<IdentityRole<Guid>, BankDbContext, Guid>>();
            // _builder.Services.AddScoped<RoleManager<IdentityRole<Guid>>>();


            _builder.Services.AddAuthentication().AddBearerToken(IdentityConstants.BearerScheme);
            _builder.Services.AddAuthorizationBuilder();

            _builder.Services.AddIdentityCore<User>()
                .AddRoles<IdentityRole<Guid>>()
                .AddEntityFrameworkStores<BankDbContext>()
                .AddApiEndpoints();


            _builder.Services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = false;
            });
        }

        private void ConfigureSwagger()
        {
            _builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc(
                    "v1",
                    new OpenApiInfo { Title = "BankHub API", Version = "v1" }
                );
                options.AddSecurityDefinition(
                    "Bearer",
                    new OpenApiSecurityScheme
                    {
                        Type = SecuritySchemeType.Http,
                        Scheme = "Bearer",
                        In = ParameterLocation.Header,
                        Description = "Please insert JWT token",
                        Name = "Authorization",
                        BearerFormat = "JWT"
                    }
                );
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                        },
                        []
                    }
                });
            });
        }
    }
}
