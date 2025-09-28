using MediatR;
using School.Core.Bases;

namespace School.Core.Features.Departments.Command.Models
{
    public class AddDepartmentCommand : IRequest<Response<string>>
    {
        public string DepartmentNameEn { get; set; }
        public string DepartmentNameAr { get; set; }
    }
}
