using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using School.Data.Commons;

namespace School.Data.Entities
{
    public class Subject : GeneralLocalizableEntity
    {
        public Subject()
        {
            StudentsSubjects = new HashSet<StudentSubject>();
            departmentsubjects = new HashSet<DepartmentSubject>();
            Ins_Subjects = new HashSet<Ins_Subject>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SubID { get; set; }
        [StringLength(500)]
        public string? SubjectNameAr { get; set; }
        public string? SubjectNameEn { get; set; }
        public DateTime? Period { get; set; }
        [InverseProperty("Subject")]
        public virtual ICollection<StudentSubject> StudentsSubjects { get; set; }
        [InverseProperty("Subject")]
        public virtual ICollection<DepartmentSubject> departmentsubjects { get; set; }
        [InverseProperty("Subject")]
        public virtual ICollection<Ins_Subject> Ins_Subjects { get; set; }
    }
}
