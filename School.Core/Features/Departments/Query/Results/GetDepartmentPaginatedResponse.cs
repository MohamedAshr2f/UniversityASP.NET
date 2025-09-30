namespace School.Core.Features.Departments.Query.Results
{
    public class GetDepartmentPaginatedResponse
    {
        public int DepartmentID { get; set; }
        public string DepartmentName { get; set; }
        public string DepartmentManager { get; set; }
        public List<InstructorDtos> instructordtos { get; set; }
        public List<StudentDtos> students { get; set; }
        public List<SubjectDtos> subjectDtos { get; set; }
        public GetDepartmentPaginatedResponse(int departmentID, string departmentName, string departmentManager, List<InstructorDtos> instructo, List<StudentDtos> stud, List<SubjectDtos> subjectD)
        {
            DepartmentID = departmentID;
            DepartmentName = departmentName;
            DepartmentManager = departmentManager;
            instructordtos = instructo;
            students = stud;
            subjectDtos = subjectD;
        }



    }
}
