using Microsoft.EntityFrameworkCore.Storage;
using WowCombatStats.Data.Repository;

namespace WowCombatStats.Data;

public class Uow : IUow
{
    private readonly DataContext _context;

    private readonly UserRepository _userRepository;

    public IUserRepository UserRepository => _userRepository;

    public Uow(DataContext context)
    {
        _context = context;

        _userRepository = new UserRepository(context);
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