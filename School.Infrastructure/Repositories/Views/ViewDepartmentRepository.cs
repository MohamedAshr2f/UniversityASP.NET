using Microsoft.EntityFrameworkCore;
using School.Data.Entities.Views;
using School.Infrastructure.Abstracts.Views;
using School.Infrastructure.ApplicationContext;
using School.Infrastructure.InfrastructureBases;

namespace School.Infrastructure.Repositories.Views
{
    public class ViewDepartmentRepository : GenericRepositoryAsync<ViewDepartment>, IViewRepository<ViewDepartment>
    {
        private DbSet<ViewDepartment> viewDepartment;
        public ViewDepartmentRepository(Context context) : base(context)
        {
            viewDepartment = context.Set<ViewDepartment>();
        }
    }
}
