using AutoMapper;
using MediatR;
using School.Core.Bases;
using School.Core.Features.Students.Command.Models;
using School.Data.Entities;
using School.Service.Abstracts;

namespace School.Core.Features.Students.Command.Handler
{
    public class StudentCommandHandler : ResponseHandler,
        IRequestHandler<AddStudentCommand, Response<string>>,
        IRequestHandler<EditStudentCommand, Response<string>>,
        IRequestHandler<DeleteStudentCommand, Response<string>>


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
            if (stud == "AddSuccefull")
            {
                return Created(stud);
            }
            return BadRequest<string>();
        }

        public async Task<Response<string>> Handle(EditStudentCommand request, CancellationToken cancellationToken)
        {
            //Check if the Id is Exist Or not
            var student = await _studentService.GetStudentByIdAsync(request.Id);

            //return NotFound
            if (student == null) return NotFound<string>();

            //mapping Between request and student
            var studentmapping = _mapper.Map<Student>(request);

            //Call service that make Edit
            var result = await _studentService.EditStudentAsync(studentmapping);

            //return response
            if (result == "Succefull")
            {
                return Updated(result);
            }
            else
            {
                return BadRequest<string>();
            }

        }

        public async Task<Response<string>> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
        {
            //Check if the Id is Exist Or not
            var student = await _studentService.GetStudentByIdwithoutAsync(request.Id);
            //return NotFound
            if (student == null) return NotFound<string>();
            //Call service that make Delete
            var result = await _studentService.DeleteStudentAsync(student);
            if (result == "Deleted") return Deleted<string>($"Studet Deleted {request.Id}");
            else return BadRequest<string>();

        }

    }
}
