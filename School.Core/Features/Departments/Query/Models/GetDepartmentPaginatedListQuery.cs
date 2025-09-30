using MediatR;
using School.Core.Features.Departments.Query.Results;
using School.Core.Wrappers;

namespace School.Core.Features.Departments.Query.Models
{
    public class GetDepartmentPaginatedListQuery : IRequest<PaginatedResult<GetDepartmentPAG>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        // public DepartmentOrderingEnum OrderBy { get; set; }
        public string? Search { get; set; }
    }
}
