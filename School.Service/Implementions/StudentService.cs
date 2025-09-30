using Microsoft.EntityFrameworkCore;
using School.Data.Entities;
using School.Data.Enums;
using School.Infrastructure.Abstracts;
using School.Service.Abstracts;

namespace School.Service.Implementions
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<string> AddStudentAsync(Student student)
        {
            //Add Student
            await _studentRepository.AddAsync(student);
            return "AddSuccefull";
        }

        public async Task<bool> IsNameARExist(string name)
        {
            //Check if student Exist Or Not
            var studentresult = _studentRepository.GetTableNoTracking().FirstOrDefault(s => s.NameAr == name);
            if (studentresult == null)
            {
                return false;
            }
            return true;
        }
        public async Task<bool> IsNameENExist(string name)
        {
            //Check if student Exist Or Not
            var studentresult = _studentRepository.GetTableNoTracking().FirstOrDefault(s => s.NameEn == name);
            if (studentresult == null)
            {
                return false;
            }
            return true;
        }

        public async Task<Student> GetStudentByIdAsync(int id)
        {
            var stud = await _studentRepository.GetTableNoTracking().Include(s => s.Department)
                 .Include(s => s.StudentSubject)
                 .ThenInclude(ss => ss.Subject)
                 .FirstOrDefaultAsync(s => s.StudID == id);
            return stud;

        }
        public async Task<Student> GetStudentByIdwithoutAsync(int id)
        {
            var stud = await _studentRepository.GetTableNoTracking()
                .FirstOrDefaultAsync(s => s.StudID == id);
            return stud;

        }

        public async Task<List<Student>> GetStudentsListAsync()
        {
            return await _studentRepository.GetStudentsListAsync();
        }


        public async Task<string> EditStudentAsync(Student student)
        {
            await _studentRepository.UpdateAsync(student);
            return "Succefull";
        }

        public async Task<bool> IsNameENExistExcludeSelf(string name, int id)
        {
            var student = await _studentRepository.GetTableNoTracking().FirstOrDefaultAsync(s => s.NameEn == name & s.StudID != id);
            if (student == null) return false;
            return true;
        }
        public async Task<bool> IsNameARExistExcludeSelf(string name, int id)
        {
            var student = await _studentRepository.GetTableNoTracking().FirstOrDefaultAsync(s => s.NameAr == name & s.StudID != id);
            if (student == null) return false;
            return true;
        }
        public async Task<string> DeleteStudentAsync(Student student)
        {
            var trans = _studentRepository.BeginTransaction();
            try
            {
                await _studentRepository.DeleteAsync(student);
                trans.Commit();
                return "Deleted";
            }
            catch (Exception ex)
            {
                await trans.RollbackAsync();
                return "Falied";
            }

        }

        public IQueryable<Student> GetStudentsQuerable()
        {
            return _studentRepository.GetTableNoTracking().Include(s => s.Department).Include(s => s.StudentSubject).ThenInclude(ss => ss.Subject).AsQueryable();
        }

        public IQueryable<Student> FilterStudentPaginatedQuerable(string search, StudentOrderingEnum orderingEnum)
        {
            var query = GetStudentsQuerable();
            if (search != null)
            {
                query = query.Where(s => s.NameEn.Contains(search) || s.Address.Contains(search));
            }
            switch (orderingEnum)
            {
                case StudentOrderingEnum.StudentID:
                    query = query.OrderBy(x => x.StudID);
                    break;

                case StudentOrderingEnum.StudentName:
                    query = query.OrderBy(x => x.NameEn);
                    break;
                case StudentOrderingEnum.Address:
                    query = query.OrderBy(x => x.Address);
                    break;
                case StudentOrderingEnum.DepartmentName:
                    query = query.OrderBy(x => x.Department.DNameEn);
                    break;
            }

            return query;
        }
    }
}
