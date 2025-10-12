using MediatR;
using Microsoft.AspNetCore.Http;
using School.Core.Bases;

namespace School.Core.Features.Instructors.Command.Models
{
    public class AddInstructorCommand : IRequest<Response<string>>
    {
        public string? NameAr { get; set; }
        public string? NameEn { get; set; }
        public string? Address { get; set; }
        public string? Position { get; set; }
        public int? SupervisorId { get; set; }
        public decimal? Salary { get; set; }
        public int DID { get; set; }
        public IFormFile? Image { get; set; }
    }
}
