using MediatR;
using School.Core.Features.Departments.Query.Results;
using School.Core.Wrappers;
using School.Data.Enums;

namespace School.Core.Features.Departments.Query.Models
{
    public class GetDepartmentPaginatedListQuery : IRequest<PaginatedResult<GetDepartmentPaginatedResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public DepartmentOrderingEnum OrderBy { get; set; }
        public string? Search { get; set; }
    }
}
