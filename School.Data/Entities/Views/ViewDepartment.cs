using Microsoft.EntityFrameworkCore;
using School.Data.Commons;

namespace School.Data.Entities.Views
{
    [Keyless]
    public class ViewDepartment : GeneralLocalizableEntity
    {
        public int DID { get; set; }

        public string? DNameAr { get; set; }

        public string? DNameEn { get; set; }
        public int StudentCount { get; set; }
    }
}
