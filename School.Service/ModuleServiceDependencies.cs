using Microsoft.Extensions.DependencyInjection;
using School.Service.Abstracts;
using School.Service.Implementions;

namespace School.Service
{
    public static class ModuleServiceDependencies
    {
        public static IServiceCollection AddServiceDependencies(this IServiceCollection services)
        {
            services.AddTransient<IStudentService, StudentService>();
            //services.AddTransient<IDepartmentService, DepartmentService>();
            return services;
        }
    }
}
