using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Localization;
using Moq;
using School.Core.Features.Students.Query.Handler;
using School.Core.Features.Students.Query.Models;
using School.Core.Features.Students.Query.Results;
using School.Core.Mapping.Students;
using School.Core.Resources;
using School.Data.Entities;
using School.Service.Abstracts;
using System.Net;

namespace School.Xunit.Core_Tests.Students.Query
{
    public class StudentQueryHandlerTest
    {
        private readonly Mock<IStudentService> _studentServiceMock;
        private readonly IMapper _mapperMock;
        private readonly Mock<IStringLocalizer<SharedResource>> _stringLocalizerMock;
        private readonly StudentProfile _studentProfile;

        public StudentQueryHandlerTest()
        {
            _studentServiceMock = new();
            _studentProfile = new();
            _stringLocalizerMock = new();
            var configuration = new MapperConfiguration(c => c.AddProfile(_studentProfile));
            _mapperMock = new Mapper(configuration);
        }

        [Fact]
        public async Task Handle_StudentList_ShoudNot_Be_Null_OR_Empty()
        {
            // Arrange
            var studentList = new List<Student>()
            {
                new Student(){ StudID=1, Address="Alex", DID=1, NameAr="محمد",NameEn="mohamed"}
            };
            var query = new GetStudentListQuery();
            _studentServiceMock.Setup(x => x.GetStudentsListAsync()).Returns(Task.FromResult(studentList));
            var handler = new StudentQueryHandler(_studentServiceMock.Object, _mapperMock, _stringLocalizerMock.Object);

            // Act
            var result = await handler.Handle(query, default);
            // Assert
            result.Data.Should().NotBeNullOrEmpty();
            result.Succeeded.Should().BeTrue();
            result.Data.Should().BeOfType<List<GetStudentListResponse>>();

        }
        [Theory]
        [InlineData(3)]
        public async Task Handle_StudentSingleByID_Where_Student_notfound_Return_statuscode404(int id)
        {
            // Arrange
            var department = new Department() { DID = 1, DNameAr = "علوم", DNameEn = "Science" };
            var studentList = new List<Student>()
            {
                new Student(){ StudID=1, Address="Alex", DID=1, NameAr="محمد",NameEn="mohamed",Department=department},
                new Student(){ StudID=2, Address="Cairo", DID=1, NameAr="علي",NameEn="Ali", Department=department}
            };
            var query = new GetStudentSingleQuery(id);
            _studentServiceMock.Setup(x => x.GetStudentByIdAsync(id)).Returns(Task.FromResult(studentList.FirstOrDefault(x => x.StudID == id)));
            var handler = new StudentQueryHandler(_studentServiceMock.Object, _mapperMock, _stringLocalizerMock.Object);
            // Act
            var result = await handler.Handle(query, default);
            // Assert
            result.StatusCode.Should().Be(HttpStatusCode.NotFound);
            result.Data.Should().BeNull();
            result.Succeeded.Should().BeFalse();

        }
    }
}
