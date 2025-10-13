using School.Core.Wrappers;

namespace School.Xunit.Wrappers.Interfaces
{
    public interface IPaginatedService<T>
    {
        public Task<PaginatedResult<T>> ReturnPaginatedListAsync(IQueryable<T> source, int pageNumber, int pageSize);
    }
}
