using Microsoft.Extensions.DependencyInjection;
using School.Infrastructure.Abstracts;
using School.Infrastructure.Repositories;

namespace School.Infrastructure
{
    public static class ModuleInfrastructureDependencies
    {
        public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services)
        {
            services.AddTransient<IDepartmentRepository, DepartmentRepository>();
            services.AddTransient<IStudentRepository, StudentRepository>();

            return services;

        }
    }
}
