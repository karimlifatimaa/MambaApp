using Mamba.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mamba.Business.Services.Abstract;

public interface ITeamServices
{
	void AddTeam(Team team);
	void RemoveTeam(int id);
	void Update(int id,Team team);
	Team GetTeam(Func<Team,bool>? func=null);
	List<Team> GetAllTeam(Func<Team, bool>? func = null);

}
