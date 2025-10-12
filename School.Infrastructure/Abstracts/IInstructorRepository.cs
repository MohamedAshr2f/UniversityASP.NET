using School.Data.Entities;
using School.Infrastructure.InfrastructureBases;

namespace School.Infrastructure.Abstracts
{
    public interface IInstructorRepository : IGenericRepositoryAsync<Instructor>
    {
    }
}
