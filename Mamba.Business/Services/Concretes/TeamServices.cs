using Mamba.Business.Exceptions;
using Mamba.Business.Services.Abstract;
using Mamba.Core.Models;
using Mamba.Core.RepositoryAbstracts;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mamba.Business.Services.Concretes;

public class TeamServices : ITeamServices
{
	private readonly ITeamRepository _repository;
	private readonly IWebHostEnvironment _webHostEnvironment;


	public TeamServices(ITeamRepository repository, IWebHostEnvironment webHostEnvironment )
	{
		_repository = repository;
		_webHostEnvironment = webHostEnvironment;
	}

	public void AddTeam(Team team)
	{
		if (!team.formFile.ContentType.Contains("image/"))   throw new FileContentException("formFile", "File content type error");

		if (team.formFile.Length > 2097152) throw new FileSizeException("formFile", "File size error");
		string path=_webHostEnvironment.WebRootPath+@"\Uploads\Team\"+team.formFile.FileName;
		using(FileStream strem=new FileStream(path, FileMode.Create))
		{
			team.formFile.CopyTo(strem);
		}
		team.ImageUrl = team.formFile.FileName;

		_repository.Add(team);
		_repository.Commit();
	}

	public List<Team> GetAllTeam(Func<Team, bool>? func = null)
	{
		return _repository.GetAll(func);
	}

	public Team GetTeam(Func<Team, bool>? func = null)
	{
		return _repository.Get(func);
	}

	public void RemoveTeam(int id)
	{
		var team = _repository.Get(x=>x.Id==id);
		if (team == null) throw new NullReferenceException();
		string path=_webHostEnvironment.WebRootPath+@"\Uploads\Team\"+ team.ImageUrl;
		if (!File.Exists(path)) throw new FileNameNotFoundException("ImageUrl","File not found");
		File.Delete(path);
		_repository.Delete(team);
		_repository.Commit();
	}

	public void Update(int id, Team team)
	{
		var oldteam=_repository.Get(x=>x.Id==id);
		if(oldteam==null) throw new NullReferenceException();
		if(team.formFile!=null)
		{
			if(!team.formFile.ContentType.Contains("image/")) throw new FileContentException("formFile","File content type error");
			if (team.formFile.Length > 2097152) throw new FileSizeException("formFile", "File size error");

			string path=_webHostEnvironment.WebRootPath + @"\Uploads\Team\" + team.formFile.FileName;
			using(FileStream stream = new FileStream(path, FileMode.Create))
			{
				team.formFile.CopyTo(stream);
			}
			oldteam.ImageUrl=team.formFile.FileName;
		}
		oldteam.FullName=team.FullName;
		oldteam.Position=team.Position;
		_repository.Commit();
	}
}
