namespace School.Core.Features.Subjects.Query.Results
{
    public class GetSubjectsListResponse
    {
        public int SubjectID { get; set; }
        public string Name { get; set; }
        public List<DepartmentDto> Department { get; set; }
    }
    public class DepartmentDto
    {
        public string DepartmentName { get; set; }
    }
}
