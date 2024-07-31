
using EFCoreDemo.Entities;
using EFCoreDemo.Service;
using Microsoft.EntityFrameworkCore;

namespace EFCoreDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            //string connectionStr = builder.Configuration["ConnectionStrings:DefaultConnection"];
            string connectionStr = builder.Configuration["DefaultConnection"];
            builder.Services.AddDbContext<ApplicationContext>(builder => builder.UseSqlServer(connectionStr));

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            

            builder.Services.AddTransient<IAccountService, AccountService>();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
