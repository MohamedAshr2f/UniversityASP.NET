namespace School.Core.Features.Departments.Query.Results
{
    public class GetDepartmentListResponse
    {
        public int DepartmentID { get; set; }
        public string DepartmentName { get; set; }
        public List<StudentDtos> students { get; set; }
        public List<SubjectDtos> subjectDtos { get; set; }

    }
    public class StudentDtos
    {
        public string StudentName { get; set; }
        public List<SubjectDtos> subjectDtos { get; set; }

    }
    public class SubjectDtos
    {
        public int SubjectID { get; set; }

        public string Name { get; set; }
    }
}
