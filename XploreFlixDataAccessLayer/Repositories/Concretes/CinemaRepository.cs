using Microsoft.AspNetCore.Http;
using XploreFlixDataAccessLayer.Contexts;
using XploreFlixDataAccessLayer.Repositories.Abstracts;
using XploreFlixDomainLayer.Entities;

namespace XploreFlixDataAccessLayer.Repositories.Concretes;

public class CinemaRepository : ICinemaRepository
{
	internal readonly XploreFlixDbContext db;
	public CinemaRepository(XploreFlixDbContext db)
	{
		this.db = db;
	}

	public int Delete(int id)
	{
		Cinema? delcin = db.Cinemas.SingleOrDefault(c => c.Id == id);
		db.Cinemas.Remove(delcin!);
		int raws = db.SaveChanges();
		return raws;
	}

	public ICollection<Cinema>? GetAll()
	{
		var cinemas = db.Cinemas.ToList();
		return cinemas;
	}

	public Cinema GetById(int id)
	{
		return db.Cinemas.SingleOrDefault(c => c.Id == id);
	}

	public Cinema GetByLocation(string location)
	{
		return db.Cinemas.SingleOrDefault(c => c.Location == location);
	}

	public Cinema GetByName(string name)
	{
		return db.Cinemas.SingleOrDefault(c => c.Name == name);
	}

	public async Task<int> InsertAsync(Cinema newCinema, List<IFormFile> Image)
	{
		foreach (var item in Image)
		{
			if (item.Length > 0)
			{
				using var stream = new MemoryStream();
				await item.CopyToAsync(stream);
				newCinema.Image = stream.ToArray();
			}
		}

		db.Cinemas.Add(newCinema);
		int raws = db.SaveChanges();
		return raws;
	}

	public async Task<int> UpdateAsync(Cinema EditCin, int id, List<IFormFile> Image)
	{
		var cinema = db.Cinemas.SingleOrDefault(c => c.Id == id);
		foreach (var item in Image)
		{
			if (item.Length > 0)
			{
				using var stream = new MemoryStream();
				await item.CopyToAsync(stream);
				EditCin.Image = stream.ToArray();
			}
		}

		cinema!.Name = EditCin.Name;
		cinema.Location = EditCin.Location;
		if (Image.Count != 0)
			cinema.Image = EditCin.Image;
		cinema.Location = EditCin.Location;
		int raws = db.SaveChanges();
		return raws;
	}
}
