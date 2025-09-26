namespace School.Core.Features.Subjects.Query.Results
{
    public class GetSubjectSingleResponse
    {
        public int SubjectID { get; set; }
        public string Name { get; set; }
        public List<DepartmentDto> Department { get; set; }
    }
}
