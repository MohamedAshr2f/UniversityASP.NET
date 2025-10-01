namespace School.Core.Features.Students.Query.Results
{
    public class GetStudentPaginatedListResponse
    {
        public int StudentID { get; set; }
        public string StudentName { get; set; }
        public string? Address { get; set; }
        public string DepartmentName { get; set; }
        public List<SubjectDtos> Subjects { get; set; }
        /*public GetStudentPaginatedListResponse(int studentID, string studentName, string? address, string departmentName, List<Subject> subjects)
        {
            StudentID = studentID;
            StudentName = studentName;
            Address = address;
            DepartmentName = departmentName;
            Subjects = subjects;
        }
        */

    }
}
