using School.Data.Entities;
using School.Infrastructure.InfrastructureBases;

namespace School.Infrastructure.Abstracts
{
    public interface IStudentRepository : IGenericRepositoryAsync<Student>
    {
        public Task<List<Student>> GetStudentsListAsync();
    }
}
