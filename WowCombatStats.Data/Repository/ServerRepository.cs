using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WowCombatStats.Models.DataModels;

namespace WowCombatStats.Data.Repository
{
	public interface IServerRepository : IRepository<Server>
	{
		
	}

	public class ServerRepository : Repository<Server>, IServerRepository
	{
		private readonly DataContext _context;

		public ServerRepository(DataContext context) : base(context)
		{
			_context = context;
		}
	}
}
