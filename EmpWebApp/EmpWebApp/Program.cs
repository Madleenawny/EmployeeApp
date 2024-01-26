
using EmpWebApp.Models;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace EmpWebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            string ConnectionString = builder.Configuration.GetConnectionString("sqlconnectionstring");
            // Add services to the container.
            builder.Services.AddDbContext<EmployeeDbContext>(optionsBuilder =>
            {
                optionsBuilder.UseSqlServer(ConnectionString);
            });

            builder.Services.AddCors((CorsOptions) =>
            {
                CorsOptions.AddPolicy("Mypolicy", (policyoptions) =>
                {
                    policyoptions.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod();
                });
            });

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors("Mypolicy");
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
