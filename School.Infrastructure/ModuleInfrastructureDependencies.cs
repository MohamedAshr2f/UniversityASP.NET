using Microsoft.Extensions.DependencyInjection;
using School.Data.Entities.Views;
using School.Infrastructure.Abstracts;
using School.Infrastructure.Abstracts.Procedures;
using School.Infrastructure.Abstracts.Views;
using School.Infrastructure.Repositories;
using School.Infrastructure.Repositories.Procedures;
using School.Infrastructure.Repositories.Views;

namespace School.Infrastructure
{
    public static class ModuleInfrastructureDependencies
    {
        public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services)
        {
            services.AddTransient<IDepartmentRepository, DepartmentRepository>();
            services.AddTransient<IStudentRepository, StudentRepository>();
            services.AddTransient<ISubjectRepository, SubjectRepository>();
            services.AddTransient<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddTransient<IViewRepository<ViewDepartment>, ViewDepartmentRepository>();

            //Procedure
            services.AddTransient<IDepartmentStudentCountProcRepository, DepartmentStudentCountProcRepository>();
            return services;

        }
    }
}
