using MediatR;
using School.Core.Features.Users.Query.Results;
using School.Core.Wrappers;

namespace School.Core.Features.Users.Query.Models
{
    public class GetUserPaginatedList : IRequest<PaginatedResult<GetUserPaginatedResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
