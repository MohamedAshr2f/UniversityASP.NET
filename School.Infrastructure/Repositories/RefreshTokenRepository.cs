using Microsoft.EntityFrameworkCore;
using School.Data.Entities.Identity;
using School.Infrastructure.Abstracts;
using School.Infrastructure.ApplicationContext;
using School.Infrastructure.InfrastructureBases;

namespace School.Infrastructure.Repositories
{
    public class RefreshTokenRepository : GenericRepositoryAsync<UserRefreshToken>, IRefreshTokenRepository
    {

        #region Fields
        private DbSet<UserRefreshToken> userRefreshToken;
        #endregion

        #region Constructors
        public RefreshTokenRepository(Context dbContext) : base(dbContext)
        {
            userRefreshToken = dbContext.Set<UserRefreshToken>();
        }
        #endregion

        #region Handle Functions

        #endregion
    }
}
