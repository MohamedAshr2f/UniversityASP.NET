using Microsoft.EntityFrameworkCore;
using School.Data.Entities;
using School.Infrastructure.Abstracts;
using School.Infrastructure.ApplicationContext;
using School.Infrastructure.InfrastructureBases;

namespace School.Infrastructure.Repositories
{
    public class InstructorRepository : GenericRepositoryAsync<Instructor>, IInstructorRepository
    {
        private readonly DbSet<Instructor> _instructors;
        public InstructorRepository(Context context) : base(context)
        {

            _instructors = context.Set<Instructor>();
        }

    }
}
