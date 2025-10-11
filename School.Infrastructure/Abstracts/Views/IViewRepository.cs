using School.Infrastructure.InfrastructureBases;

namespace School.Infrastructure.Abstracts.Views
{
    public interface IViewRepository<T> : IGenericRepositoryAsync<T> where T : class
    {

    }
}
