using Microsoft.AspNetCore.Http;
using XploreFlixDataAccessLayer.Contexts;
using XploreFlixDataAccessLayer.Repositories.Abstracts;
using XploreFlixDomainLayer.Entities;

namespace XploreFlixDataAccessLayer.Repositories.Concretes;

public class UpdateProfileRepository : IUpdateProfileRepository
{
	internal readonly XploreFlixDbContext? db;
	public UpdateProfileRepository(XploreFlixDbContext? db)
	{
		this.db = db;
	}
	public User GetById(string? id)
	{
		var user = db!.Users.FirstOrDefault(x => x.Id == id);
		return user!;
	}

	public async Task<int> InsertAsync(User NewUser, List<IFormFile> Image)
	{
		foreach (var item in Image)
		{
			if (item.Length > 0)
			{
				using var stream = new MemoryStream();
				await item.CopyToAsync(stream);
				NewUser.Image = stream.ToArray();
			}
		}
		db!.Add(NewUser);
		return db.SaveChanges();
	}

	public async Task<int> UpdateAsync(string? id, User UpdateUser, List<IFormFile> Image)
	{
		foreach (var item in Image)
		{
			if (item.Length > 0)
			{
				using var stream = new MemoryStream();
				await item.CopyToAsync(stream);
				UpdateUser.Image = stream.ToArray();
			}
		}
		var user = db!.Users.SingleOrDefault(u => u.Id == id);

		user!.FullName = UpdateUser.FullName;
		if (Image.Count != 0)
		{
			user.Image = UpdateUser.Image;
		}
		int raws = db.SaveChanges();
		return raws;
	}
}
