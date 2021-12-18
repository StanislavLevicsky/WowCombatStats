using Microsoft.EntityFrameworkCore.Storage;

namespace WowCombatStats.Data;

public interface IUow
{
    IDbContextTransaction BeginTransaction();
    Task<IDbContextTransaction> BeginTransactionAsync();
    void SaveChange();
    Task SaveChangeAsync();
}