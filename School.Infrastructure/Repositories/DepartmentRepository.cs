using Microsoft.EntityFrameworkCore;
using School.Data.Entities;
using School.Infrastructure.Abstracts;
using School.Infrastructure.ApplicationContext;
using School.Infrastructure.InfrastructureBases;

namespace School.Infrastructure.Repositories
{
    public class DepartmentRepository : GenericRepositoryAsync<Department>, IDepartmentRepository
    {
        private readonly DbSet<Department> _departments;

        public DepartmentRepository(Context context) : base(context)
        {
            _departments = context.Set<Department>();
        }

    }
}
