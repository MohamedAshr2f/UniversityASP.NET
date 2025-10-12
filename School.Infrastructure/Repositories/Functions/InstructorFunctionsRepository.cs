using System.Data;
using Microsoft.EntityFrameworkCore;
using School.Infrastructure.Abstracts.Functions;
using School.Infrastructure.ApplicationContext;

namespace School.Infrastructure.Repositories.Functions
{
    public class InstructorFunctionsRepository : IInstructorFunctionsRepository
    {

        #region Fileds
        private readonly Context _context;
        #endregion
        #region Constructors
        public InstructorFunctionsRepository(Context context)
        {
            _context = context;
        }

        #endregion
        #region Handle Functions
        public decimal GetSalarySummationOfInstructor(string query)
        {
            using (var cmd = _context.Database.GetDbConnection().CreateCommand())
            {
                if (cmd.Connection.State != ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }
                decimal response = 0;
                cmd.CommandText = query;
                var value = cmd.ExecuteScalar();
                var result = value.ToString();
                if (decimal.TryParse(result, out decimal d))
                {
                    response = d;
                }
                cmd.Connection.Close();
                return response;
            }
        }
        #endregion
    }
}
