namespace School.Core.Features.Departments.Query.Results
{
    public class GetDepartmentPAG
    {


        public int DepartmentID { get; set; }
        public string DepartmentName { get; set; }
        public string DepartmentManager { get; set; }
        public GetDepartmentPAG(int deptId, string dname, string dman)
        {
            DepartmentID = deptId;
            DepartmentName = dname;
            DepartmentManager = dman;
        }
    }
}
