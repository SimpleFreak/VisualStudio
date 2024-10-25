using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using PensionFund.API.Extensions;
using PensionFund.Core.Abstractions;
using PensionFund.DataAccess;
using PensionFund.DataAccess.Repositories;
using PensionFund.Infrastructure;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.Extensions.Configuration;
using PensionFund.Application.Interfaces.Auth;
using PensionFund.Application.Services;
using PensionFund.Modules.Functionality;

namespace PensionFund.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string excelFile = "../PensionFund.Modules/ExcelFiles/TestExcel.xlsx";
            ParsingDataExcel.PrintData(excelFile);

            var builder = WebApplication.CreateBuilder(args);
            var services = builder.Services;
            var configuration = builder.Configuration;

            services.AddApiAuthentication(configuration);

            services.AddControllers();
            services.AddSwaggerGen();

            services.Configure<JwtOptions>(configuration.GetSection(nameof(JwtOptions)));

            builder.Services.AddDbContext<PensionFundDbContext>(options =>
                options.UseMySQL(builder.Configuration.GetConnectionString("DefaultConnection")));

            services.AddEndpointsApiExplorer();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<UserService>();

            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<PersonService>();

            services.AddScoped<IReportRepository, ReportRepository>();
            services.AddScoped<ReportService>();

            services.AddScoped<IPredictionRepository, PredictionRepository>();
            services.AddScoped<PredictionService>();

            services.AddScoped<IJwtProvider, JwtProvider>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();

            services.AddCors(options =>
            {
                options.AddPolicy("AspNetApp", policyBuilder =>
                {
                    policyBuilder.WithOrigins("147.45.110.199:3306");
                    policyBuilder.AllowAnyHeader();
                    policyBuilder.AllowAnyMethod();
                    policyBuilder.AllowCredentials();
                });

                options.AddPolicy("NextApp", policyBuilder =>
                {
                    policyBuilder.WithOrigins("localhost");
                    policyBuilder.AllowAnyHeader();
                    policyBuilder.AllowAnyMethod();
                    policyBuilder.AllowCredentials();
                });
            });

            var app = builder.Build();

            if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {

                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                    c.RoutePrefix = string.Empty;
                });
            }

            app.UseHttpsRedirection();

            app.UseCookiePolicy(new CookiePolicyOptions()
            {
                MinimumSameSitePolicy = SameSiteMode.Strict,
                HttpOnly = HttpOnlyPolicy.Always,
                Secure = CookieSecurePolicy.Always
            });

            app.UseAuthentication();
            app.UseAuthorization();

            app.AddMappedEndpoints();

            app.UseCors("AspNetApp");
            app.UseCors("NextApp");

            app.MapGet("/", () => "Hello ForwardedHeadersOptions!");



            app.Run();
        }
    }
}
