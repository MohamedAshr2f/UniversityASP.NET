using Microsoft.EntityFrameworkCore;
using School.Data.Entities;
using School.Infrastructure.Abstracts;
using School.Infrastructure.ApplicationContext;
using School.Infrastructure.InfrastructureBases;

namespace School.Infrastructure.Repositories
{
    public class SubjectRepository : GenericRepositoryAsync<Subject>, ISubjectRepository
    {
        public readonly DbSet<Subject> _Subjects;


        public SubjectRepository(Context context) : base(context)
        {
            _Subjects = context.Set<Subject>();
        }

        public async Task<List<Subject>> GetSubjectListAsync()
        {
            var subjectList = await _Subjects.Include(s => s.DepartmetsSubjects).ThenInclude(d => d.Department).ToListAsync();
            return subjectList;
        }
    }
}
