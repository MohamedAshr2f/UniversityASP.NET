using School.Core.Wrappers;
using School.Data.Entities;
using School.Xunit.Wrappers.Interfaces;

namespace School.Xunit.Wrappers.Implementations
{
    public class PaginatedService : IPaginatedService<Student>
    {
        public async Task<PaginatedResult<Student>> ReturnPaginatedListAsync(IQueryable<Student> source, int pageNumber, int pageSize)
        {
            return await source.ToPaginatedListAsync(pageNumber, pageSize);

        }
    }
}
