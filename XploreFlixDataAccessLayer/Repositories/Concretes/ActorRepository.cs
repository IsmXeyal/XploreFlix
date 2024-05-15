using Microsoft.AspNetCore.Http;
using XploreFlixDataAccessLayer.Contexts;
using XploreFlixDataAccessLayer.Repositories.Abstracts;
using XploreFlixDomainLayer.Entities;

namespace XploreFlixDataAccessLayer.Repositories.Concretes;

public class ActorRepository : IActorRepository
{
	internal readonly XploreFlixDbContext? db;
	public ActorRepository(XploreFlixDbContext? _db)
	{
		db = _db;
	}

	public int Delete(int id)
	{
		Actor? DelAct = db!.Actors.SingleOrDefault(n => n.Id == id);
		db.Actors.Remove(DelAct!);
		int raws = db.SaveChanges();
		return raws;
	}

	public ICollection<Actor>? GetAll()
	{
		var Actors = db!.Actors.ToList();
		return Actors;
	}

	public Actor GetById(int id)
	{
		return db.Actors.SingleOrDefault(n => n.Id == id);
	}

	public Actor GetByName(string name)
	{
		return db.Actors.SingleOrDefault(n => n.Name == name);
	}

	public async Task<int> InsertAsync(Actor newActor, List<IFormFile> Image)
	{
		// There is problem here
		foreach (var item in Image)
		{
			if (item.Length > 0)
			{
				using var stream = new MemoryStream();
				await item.CopyToAsync(stream);
				newActor.Image = stream.ToArray();
			}
		}
		db!.Actors.Add(newActor);
		int raws = db.SaveChanges();
		return raws;
	}

	public async Task<int> UpdateAsync(Actor EditActor, int id, List<IFormFile> Image)
	{
		var Actor = db!.Actors.SingleOrDefault(n => n.Id == id);
		foreach (var item in Image)
		{
			if (item.Length > 0)
			{
				using var stream = new MemoryStream();
				await item.CopyToAsync(stream);
				EditActor.Image = stream.ToArray();
			}
		}
		Actor!.Id = EditActor.Id;
		Actor.Name = EditActor.Name;
		if (Image.Count != 0)
		{
			Actor.Image = EditActor.Image;
		}
		Actor.Bio = EditActor.Bio;
		int raws = db.SaveChanges();
		return raws;
	}
}
