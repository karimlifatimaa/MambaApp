using Mamba.Business.Exceptions;
using Mamba.Business.Services.Abstract;
using Mamba.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Mamba_App.Areas.Admin.Controllers
{
		[Area("Admin")]
	public class TeamController : Controller
	{
		private readonly ITeamServices _services;

		public TeamController(ITeamServices services)
		{
			_services = services;
		}

		public IActionResult Index()
		{
			var teams=_services.GetAllTeam();
			return View(teams);
		}
		public IActionResult Create()
		{
			return View();
		}
		[HttpPost]
        public IActionResult Create(Team team)
        {
			if (!ModelState.IsValid)
			{
				return View();
			}
			if (team == null) throw new TeamNotFoundException("Team not found!!!");
			try
			{
                _services.AddTeam(team);
            }
			catch (FileContentException ex)
			{
				ModelState.AddModelError(ex.PropertyName,ex.Message);
				return View();
				throw;
			}
			catch(FileSizeException ex) 
			{
				ModelState.AddModelError(ex.PropertyName,ex.Message);
				return View();	
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
            return RedirectToAction("Index");
        }
		public IActionResult Update(int id)
		{
			var team =_services.GetTeam(x=>x.Id==id);
			if (team == null) throw new NullReferenceException();
			return View(team);

		}
		[HttpPost]
		public IActionResult Update(Team team)
		{
			if(!ModelState.IsValid) 
			{
				return View();
			}
            try
            {
                _services.Update(team.Id,team);
            }
            catch (FileContentException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
                throw;
            }
            catch (FileSizeException ex)
            {
                ModelState.AddModelError(ex.PropertyName, ex.Message);
                return View();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return RedirectToAction("Index");
        }
		public IActionResult Delete(int id)
		{
			var item=_services.GetTeam(x=> x.Id==id);	
			if(item == null) throw new NullReferenceException();
			//try
			//{
			//	_services.RemoveTeam(item.Id);

			//}
			//catch (FileNameNotFoundException ex)
			//{
			//	ModelState.AddModelError(ex.PropertyName,ex.Message);
			//	return RedirectToAction("index");
			//}
			//catch(Exception ex)
			//{
			//	return BadRequest();
			//}
			_services.RemoveTeam(item.Id);
			return RedirectToAction("Index");
		}
    }
}
