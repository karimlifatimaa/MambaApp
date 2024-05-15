using Microsoft.AspNetCore.Mvc;

namespace Mamba_App.Areas.Admin.Controllers
{
	public class DashboardController : Controller
	{
		[Area("Admin")]
		public IActionResult Index()
		{
			return View();
		}
	}
}
