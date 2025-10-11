using MediatR;
using School.Core.Bases;
using School.Core.Features.Departments.Query.Results;

namespace School.Core.Features.Departments.Query.Models
{
    public class GetDepartmentStudentCountByIDQuery : IRequest<Response<GetDepartmentStudentCountByIDResponse>>
    {
        public int DID { get; set; }

        public GetDepartmentStudentCountByIDQuery(int dID)
        {
            DID = dID;
        }
    }
}
