using Mamba.Business.Services.Abstract;
using Mamba.Core.Models;

using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Mamba_App.Controllers
{
	public class HomeController : Controller
	{
		
		private readonly ITeamServices _services;

        public HomeController(ITeamServices services)
        {
            _services = services;
        }

        public IActionResult Index()
		{
			var teams = _services.GetAllTeam();
			return View(teams);
		}

		
	}
}