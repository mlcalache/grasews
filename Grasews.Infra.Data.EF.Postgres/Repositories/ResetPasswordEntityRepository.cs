using Grasews.Domain.Entities;
using Grasews.Domain.Interfaces.Repositories;

namespace Grasews.Infra.Data.EF.Postgres.Repositories
{
    public class ResetPasswordEntityRepository : BaseEntityRepository<ResetPassword, int>, IResetPasswordEntityRepository
    {
    }
}