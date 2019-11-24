using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Mobiclone.Api.Database;
using Mobiclone.Api.Lib;
using System.Text;

namespace Mobiclone.Api
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = _configuration["ConnectionStrings:Default"];

            services.AddDbContext<MobicloneContext>(options => options.UseSqlServer(connectionString));

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("Auth:Secret")));

            var issuer = _configuration.GetValue<string>("Auth:Issuer");

            var audience = _configuration.GetValue<string>("Auth:Audience");

            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters.ValidIssuer = issuer;
                    options.TokenValidationParameters.ValidAudience = audience;
                    options.TokenValidationParameters.IssuerSigningKey = secretKey;
                    options.TokenValidationParameters.ValidateIssuer = true;
                    options.TokenValidationParameters.ValidateAudience = true;
                    options.TokenValidationParameters.ValidateIssuerSigningKey = true;
                });

            services.AddScoped<IHash, Bcrypt>();

            services.AddScoped<IAuth>((provider) => new Jwt(context: provider.GetService<MobicloneContext>(), hash: provider.GetService<IHash>(), issuer, audience, secretKey));

            services.AddMvc(options => options.EnableEndpointRouting = false);

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Mobiclone",
                    Description = "Mobiclone",
                    Version = "v1"
                });

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();

            app.UseMvcWithDefaultRoute();

            app.UseSwagger();

            app.UseSwaggerUI(setup =>
            {
                setup.SwaggerEndpoint("/swagger/v1/swagger.json", "Mobiclone");
                setup.RoutePrefix = string.Empty;
            });
        }
    }
}
