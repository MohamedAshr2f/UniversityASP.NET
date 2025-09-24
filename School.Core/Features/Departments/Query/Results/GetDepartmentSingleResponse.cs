namespace School.Core.Features.Departments.Query.Results
{
    public class GetDepartmentSingleResponse
    {
        public string DepartmentName { get; set; }
        public List<StudentDtos> StudentDtos { get; set; }
        public List<SubjectDtos> SubjectDtos { get; set; }

    }

}
