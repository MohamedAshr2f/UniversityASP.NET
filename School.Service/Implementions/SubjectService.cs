using Microsoft.EntityFrameworkCore;
using School.Data.Entities;
using School.Infrastructure.Abstracts;
using School.Service.Abstracts;

namespace School.Service.Implementions
{
    public class SubjectService : ISubjectService
    {
        private readonly ISubjectRepository _subjectRepository;

        public SubjectService(ISubjectRepository subjectRepository)
        {

            _subjectRepository = subjectRepository;
        }

        public async Task<List<Subject>> GetSubjectListAsync()
        {
            var subjectList = await _subjectRepository.GetSubjectListAsync();
            return subjectList;

        }
        public async Task<Subject> GetSubjectByIdAsync(int id)
        {
            var subject = await _subjectRepository.GetTableNoTracking().Include(s => s.DepartmetsSubjects)
                .ThenInclude(ds => ds.Department).FirstOrDefaultAsync(s => s.SubID == id);
            return subject;
        }
    }
}
