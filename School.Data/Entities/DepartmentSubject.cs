using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace School.Data.Entities
{
    public class DepartmentSubject
    {
        // public int DeptSubID { get; set; }
        [Key]
        public int DID { get; set; }
        [Key]
        public int SubID { get; set; }

        [ForeignKey("DID")]
        public virtual Department Department { get; set; }

        [ForeignKey("SubID")]
        public virtual Subject Subjects { get; set; }
    }
}
