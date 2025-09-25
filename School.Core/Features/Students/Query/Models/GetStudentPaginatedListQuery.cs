using MediatR;
using School.Core.Features.Students.Query.Results;
using School.Core.Wrappers;

namespace School.Core.Features.Students.Query.Models
{
    public class GetStudentPaginatedListQuery : IRequest<PaginatedResult<GetStudentPaginatedListResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        // public string?[] OrderBy { get; set; }
        public string? Search { get; set; }

    }
}
