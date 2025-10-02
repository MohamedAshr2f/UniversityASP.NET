using MediatR;
using School.Core.Bases;
using School.Core.Features.Users.Query.Results;

namespace School.Core.Features.Users.Query.Models
{
    public class GetUserSingle : IRequest<Response<GetUserSingleResponse>>
    {
        public int ID { get; set; }
        public GetUserSingle(int id)
        {
            ID = id;
        }
    }
}
