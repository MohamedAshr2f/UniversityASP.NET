namespace School.Core.Features.Students.Query.Results
{
    public class GetStudentSingleResponse
    {
        public int StudentID { get; set; }
        public string StudentName { get; set; }
        public string DepartmentName { get; set; }
        public List<SubjectDto> Subjects { get; set; }

    }
    public class SubjectDto
    {
        public int SubjectID { get; set; }
        public string Name { get; set; }
    }

}
