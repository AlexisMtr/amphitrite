using Amphitrite.Configuration;
using Amphitrite.Configuration.MapperProfiles;
using Amphitrite.Models;
using Amphitrite.Repositories;
using Amphitrite.Repositories.SQL;
using Amphitrite.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Amphitrite
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
            services.AddApplicationInsightsTelemetry();
            services.AddLogging(c =>
             {
                 c.SetMinimumLevel(LogLevel.Information);
                 c.AddFilter(nameof(Amphitrite), LogLevel.Trace);
             });

            services.Configure<IssuerSigningKeySettings>(Configuration.GetSection("IssuerSigningKey"));

            services.AddDbContext<AmphitriteContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<AmphitriteContext>();

            services.AddScoped<IAlarmRepository, AlarmRepository>();
            services.AddScoped<IPoolRepository, PoolRepository>();
            services.AddScoped<ITelemetryRepository, TelemetryRepository>();
            services.AddScoped<IDeviceRepository, DeviceRepository>();
            services.AddScoped<UserManager<User>>();
            services.AddScoped<RoleManager<IdentityRole>>();
            services.AddScoped<SignInManager<User>>();

            services.AddScoped<AlarmService>();
            services.AddScoped<PoolService>();
            services.AddScoped<TelemetryService>();
            services.AddScoped<UserService>();
            services.AddScoped<DeviceService>();

            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                byte[] key = Encoding.ASCII.GetBytes(Configuration.GetSection("IssuerSigningKey")["SigningKey"]);
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                };
            });

            services.AddSwaggerGen(c =>
            {
                c.DescribeAllParametersInCamelCase();
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Amphitrite",
                    Version = "1.0.0",
                    Description = "API for connected swimmingpool",
                    Contact = new OpenApiContact
                    {
                        Name = "AlexisMtr",
                        Url = new Uri("https://github.com/AlexisMtr")
                    }
                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "" +
                    "JWT Authorization header using the Bearer scheme. \r\n\r\n " +
                    "Enter    'Bearer' [space] and then your token in the text input below.\r\n\r\n" +
                    "Example:      \"Bearer abcdef1234\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" },
                            In = ParameterLocation.Header
                        },
                        new List<string>()
                    }
                });
            });

            services.AddCors();
            services.AddAutoMapper(config =>
            {
                config.AddProfile<PoolProfile>();
                config.AddProfile<AlarmProfile>();
                config.AddProfile<TelemetryProfile>();
                config.AddProfile<DeviceProfile>();
            });
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Amphitrite v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
