using Microsoft.EntityFrameworkCore.Storage;
using WowCombatStats.Data.Repository;

namespace WowCombatStats.Data;

public interface IUow
{
    IUserRepository UserRepository {  get; }

    IDbContextTransaction BeginTransaction();
    Task<IDbContextTransaction> BeginTransactionAsync();
    void SaveChange();
    Task SaveChangeAsync();
}