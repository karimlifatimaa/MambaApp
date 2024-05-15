using Mamba.Core.Models;
using Mamba.Core.RepositoryAbstracts;
using Mamba.Data.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mamba.Data.RepositoryConcretes;

public class TeamRepository : GenericRepository<Team>, ITeamRepository
{
	public TeamRepository(AppDbContext appDbContext) : base(appDbContext)
	{
	}
}
