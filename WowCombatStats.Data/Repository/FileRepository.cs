using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WowCombatStats.Data.Repository
{
    public interface IFileRepository : IRepository<WowCombatStats.Models.DataModels.File>
    {

    }

    public class FileRepository : Repository<WowCombatStats.Models.DataModels.File>, IFileRepository
    {
        private readonly DataContext _context;

        public FileRepository(DataContext context) : base(context)
        {
            _context = context;
        }
    }
}
