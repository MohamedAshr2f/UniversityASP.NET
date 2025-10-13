using AutoMapper;
using EntityFrameworkCore.Testing.Common;
using FluentAssertions;
using Microsoft.Extensions.Localization;
using Moq;
using School.Core.Features.Students.Query.Handler;
using School.Core.Features.Students.Query.Models;
using School.Core.Features.Students.Query.Results;
using School.Core.Mapping.Students;
using School.Core.Resources;
using School.Data.Entities;
using School.Data.Enums;
using School.Service.Abstracts;
using System.Net;
[assembly: CollectionBehavior(CollectionBehavior.CollectionPerClass, MaxParallelThreads = 6)]

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
        [Theory]
        [InlineData(1)]
        public async Task Handle_StudentSingleByID_Where_Student_Found_Return_statuscode200(int id)
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
            result.StatusCode.Should().Be(HttpStatusCode.OK);
            result.Data.Should().NotBeNull();
            result.Succeeded.Should().BeTrue();
            result.Data.Should().BeOfType<GetStudentSingleResponse>();
            result.Data.StudentID.Should().Be(id);
            result.Data.StudentName.Should().Be(studentList.FirstOrDefault(x => x.StudID == id).NameEn);


        }
        [Fact]
        public async Task Handle_StudentPaginated_Should_NotNull_And_NotEmpty()
        {
            //arrange
            var department = new Department() { DID = 1, DNameAr = "علوم", DNameEn = "Science" };
            var studentList = new AsyncEnumerable<Student>(new List<Student>
            {
                new Student(){ StudID=1, Address="Alex", DID=1, NameAr="محمد",NameEn="mohamed",Department=department},
                new Student(){ StudID=2, Address="Cairo", DID=1, NameAr="علي",NameEn="Ali", Department=department},
                new Student(){ StudID=3, Address="Giza", DID=1, NameAr="يوسف",NameEn="Youssef", Department=department},
                new Student(){ StudID=4, Address="Tanta", DID=1, NameAr="اسلام",NameEn="Islam", Department=department},
                new Student(){ StudID=5, Address="Mansoura", DID=1, NameAr="محمود",NameEn="Mahmoud", Department=department}
            });
            var query = new GetStudentPaginatedListQuery() { PageNumber = 1, PageSize = 2, OrderBy = StudentOrderingEnum.StudentID, Search = "mohamed" };
            _studentServiceMock.Setup(x => x.FilterStudentPaginatedQuerable(query.Search, query.OrderBy)).Returns(studentList.AsQueryable());
            var handler = new StudentQueryHandler(_studentServiceMock.Object, _mapperMock, _stringLocalizerMock.Object);

            //act
            var result = await handler.Handle(query, default);

            //assert
            result.Data.Should().NotBeNullOrEmpty();
            result.Data.Should().BeOfType<List<GetStudentPaginatedListResponse>>();

        }

        private IQueryable<T> AsyncEnumerable<T>()
        {
            throw new NotImplementedException();
        }
    }
}
