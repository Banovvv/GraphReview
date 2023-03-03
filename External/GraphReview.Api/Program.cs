using GraphReview.Api.Middlewares;
using GraphReview.Application;
using GraphReview.Infrastructure;
using GraphReview.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GraphReview.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            {
                builder.Services.AddSwaggerGen();
                builder.Services.AddControllers();
                builder.Services.AddEndpointsApiExplorer();

                builder.Services.AddTransient<GlobalExceptionHandlingMiddleware>();

                builder.Services.AddDbContext<ApplicationDbContext>(options =>
                {
                    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
                });

                builder.Services.AddApplicationServices();
                builder.Services.AddInfrastructureServices();
            }

            var app = builder.Build();
            {
                // Configure the HTTP request pipeline.
                if (app.Environment.IsDevelopment())
                {
                    app.UseSwagger();
                    app.UseSwaggerUI();
                }

                app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

                app.UseHttpsRedirection();

                app.UseAuthorization();

                app.MapControllers();

                app.Run();
            }
        }
    }
}