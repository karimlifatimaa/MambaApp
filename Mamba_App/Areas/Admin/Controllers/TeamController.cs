using Mamba.Business.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace Mamba_App.Areas.Admin.Controllers
{
	public class TeamController : Controller
	{
		private readonly ITeamServices _services;

		public TeamController(ITeamServices services)
		{
			_services = services;
		}

		[Area("Admin")]
		public IActionResult Index()
		{
			var teams=_services.GetAllTeam();
			return View(teams);
		}
	}
}
