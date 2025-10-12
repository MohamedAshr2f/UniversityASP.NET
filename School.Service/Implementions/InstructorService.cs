using School.Infrastructure.Abstracts.Functions;
using School.Service.Abstracts;

namespace School.Service.Implementions
{
    public class InstructorService : IInstructorService
    {
        private readonly IInstructorFunctionsRepository _instructorFunctionsRepository;

        public InstructorService(IInstructorFunctionsRepository instructorFunctionsRepository)
        {
            _instructorFunctionsRepository = instructorFunctionsRepository;
        }
        public async Task<decimal> GetSalarySummationOfInstructor()
        {
            decimal result = 0;
            result = _instructorFunctionsRepository.GetSalarySummationOfInstructor("select dbo.GetSalarySummation()");
            return result;
        }
    }
}
