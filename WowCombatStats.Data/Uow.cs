using Microsoft.EntityFrameworkCore.Storage;
using WowCombatStats.Data.Repository;

namespace WowCombatStats.Data;

public class Uow : IUow
{
    private readonly DataContext _context;

    private readonly UserRepository _userRepository;
    private readonly ServerRepository _serverRepository;
    private readonly FileRepository _fileRepository;

    public IUserRepository UserRepository => _userRepository;
    public IServerRepository ServerRepository => _serverRepository;
    public IFileRepository FileRepository => _fileRepository;

    public Uow(DataContext context)
    {
        _context = context;

        _userRepository = new UserRepository(context);
        _serverRepository = new ServerRepository(context);
        _fileRepository = new FileRepository(context);
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