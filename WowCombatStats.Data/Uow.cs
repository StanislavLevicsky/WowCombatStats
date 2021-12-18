using Microsoft.EntityFrameworkCore.Storage;

namespace WowCombatStats.Data;

public class Uow : IUow
{
    private readonly DataContext _context;

    public Uow(DataContext context)
    {
        _context = context;
    }

    public IDbContextTransaction BeginTransaction()
    {
        return _context.Database.BeginTransaction();
    }    
    
    public async Task<IDbContextTransaction> BeginTransactionAsync()
    {
        return await _context.Database.BeginTransactionAsync();
    }

    public void SaveChange()
    {
        _context.SaveChanges();
    }

    public async Task SaveChangeAsync()
    {
        await _context.SaveChangesAsync();
    }
}