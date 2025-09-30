using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using School.Data.Commons;

namespace School.Data.Entities
{

    public partial class Department : GeneralLocalizableEntity
    {
        public Department()
        {
            Students = new HashSet<Student>();
            departmentsubjects = new HashSet<DepartmentSubject>();
            Instructors = new HashSet<Instructor>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DID { get; set; }
        //[StringLength(500)]
        public string? DNameAr { get; set; }
        [StringLength(200)]
        public string? DNameEn { get; set; }

        public int? InsManager { get; set; }

        [InverseProperty("Department")]
        public virtual ICollection<Student>? Students { get; set; }
        [InverseProperty("Department")]
        public virtual ICollection<DepartmentSubject>? departmentsubjects { get; set; }
        [InverseProperty("department")]
        public virtual ICollection<Instructor>? Instructors { get; set; }

        [ForeignKey("InsManager")]
        [InverseProperty("departmentManager")]
        public virtual Instructor? Instructor { get; set; }

    }
}
