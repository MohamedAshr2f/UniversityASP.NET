namespace School.Core.Features.Students.Query.Results
{
    public class GetStudentListResponse
    {
        public int StudentID { get; set; }
        public string StudentName { get; set; }
        public string DepartmentName { get; set; }
        public List<SubjectDtos> SubjectDtos { get; set; }

    }
    public class SubjectDtos
    {
        public int SubjectID { get; set; }
        public string Name { get; set; }
    }

}
