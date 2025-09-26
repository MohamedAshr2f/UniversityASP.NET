using School.Data.Entities;

namespace School.Service.Abstracts
{
    public interface ISubjectService
    {
        public Task<List<Subject>> GetSubjectListAsync();
        public Task<Subject> GetSubjectByIdAsync(int id);
    }
}
