using School.Data.Entities.Procedures;
using School.Infrastructure.Abstracts.Procedures;
using School.Infrastructure.ApplicationContext;
using StoredProcedureEFCore;

namespace School.Infrastructure.Repositories.Procedures
{
    public class DepartmentStudentCountProcRepository : IDepartmentStudentCountProcRepository
    {
        private readonly Context _context;

        public DepartmentStudentCountProcRepository(Context context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<DepartmentStudentCountProc>> GetDepartmentStudentCountProcs(DepartmentStudentCountProcParameters parameters)
        {
            var rows = new List<DepartmentStudentCountProc>();
            await _context.LoadStoredProc(nameof(DepartmentStudentCountProc))
                   .AddParam(nameof(DepartmentStudentCountProcParameters.DID), parameters.DID)
                   .ExecAsync(async r => rows = await r.ToListAsync<DepartmentStudentCountProc>());
            return rows;
        }
    }
}
