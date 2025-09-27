using MediatR;
using School.Core.Features.Students.Query.Results;
using School.Core.Wrappers;
using School.Data.Enums;

namespace School.Core.Features.Students.Query.Models
{
    public class GetStudentPaginatedListQuery : IRequest<PaginatedResult<GetStudentPaginatedListResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public StudentOrderingEnum OrderBy { get; set; }
        public string? Search { get; set; }

    }
}
