using EntityFrameworkCore.Testing.Common;
using FluentAssertions;
using Moq;
using School.Core.Wrappers;
using School.Data.Entities;
using School.Xunit.Wrappers.Interfaces;


namespace School.Xunit.Services_Test.ExtensionMethods
{
    public class ExtensionMethodTest
    {
        private readonly Mock<IPaginatedService<Student>> _paginatedServiceMock;
        public ExtensionMethodTest()
        {
            _paginatedServiceMock = new();
        }
        [Theory]
        [InlineData(1, 10)]
        public async Task ToPaginatedListAsync_Should_Return_List(int pageNumber, int pageSize)
        {
            //Arrange

            var department = new Department() { DID = 1, DNameAr = "هندسة البرمجيات", DNameEn = "SE" };

            var studentList = new AsyncEnumerable<Student>(new List<Student>
            {
                new Student(){ StudID=1, Address="Alex", DID=1, NameAr="محمد",NameEn="mohamed",Department=department}
            });
            var paginatedResult = new PaginatedResult<Student>(studentList.ToList());
            _paginatedServiceMock.Setup(x => x.ReturnPaginatedListAsync(studentList, pageNumber, pageSize)).Returns(Task.FromResult(paginatedResult));
            //Act
            var result = await _paginatedServiceMock.Object.ReturnPaginatedListAsync(studentList, pageNumber, pageSize);
            //Assert
            result.Data.Should().NotBeNullOrEmpty();

        }
    }
}
