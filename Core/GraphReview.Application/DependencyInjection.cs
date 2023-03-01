using GraphReview.Application.Abstractions.Departments;
using GraphReview.Application.Abstractions.Email;
using GraphReview.Application.Abstractions.Employees;
using GraphReview.Application.Abstractions.Reviews;
using GraphReview.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace GraphReview.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IReviewService, ReviewService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IDepartmentService, DepartmentService>();

            return services;
        }
    }
}
