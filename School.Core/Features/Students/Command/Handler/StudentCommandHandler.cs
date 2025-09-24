using AutoMapper;
using MediatR;
using School.Core.Bases;
using School.Core.Features.Students.Command.Models;
using School.Data.Entities;
using School.Service.Abstracts;

namespace School.Core.Features.Students.Command.Handler
{
    public class StudentCommandHandler : ResponseHandler, IRequestHandler<AddStudentCommand, Response<string>>
    {
        private readonly IMapper _mapper;
        private readonly IStudentService _studentService;

        public StudentCommandHandler(IMapper mapper, IStudentService studentService)
        {
            _mapper = mapper;
            _studentService = studentService;
        }
        public async Task<Response<string>> Handle(AddStudentCommand request, CancellationToken cancellationToken)
        {
            //mapping Between Request And Student
            var studentmapper = _mapper.Map<Student>(request);
            //Add
            var stud = await _studentService.AddStudentAsync(studentmapper);
            if (stud == "Exist")
            {
                return UnprocessableEntity<string>("Name is exist");
            }
            else if (stud == "AddSuccefull")
            {
                return Created(stud);
            }
            return BadRequest<string>();
        }
    }
}
