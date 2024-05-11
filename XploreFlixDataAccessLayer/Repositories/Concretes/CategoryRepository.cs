using Microsoft.AspNetCore.Http;
using XploreFlixDataAccessLayer.Contexts;
using XploreFlixDataAccessLayer.Repositories.Abstracts;
using XploreFlixDomainLayer.Entities;

namespace XploreFlixDataAccessLayer.Repositories.Concretes;

public class CategoryRepository : ICategoryRepository
{
	internal readonly XploreFlixDbContext db;
	public CategoryRepository(XploreFlixDbContext db)
	{
		this.db = db;
	}
	public int Delete(int id)
	{
		Category? delCategory = db.Categories.SingleOrDefault(c => c.Id == id);
		db.Categories.Remove(delCategory!);
		int raws = db.SaveChanges();
		return raws;
	}

	public ICollection<Category>? GetAll()
	{
		var category = db.Categories.ToList();
		return category;
	}

	public Category GetById(int id)
	{
		return db.Categories.SingleOrDefault(c => c.Id == id);
	}

	public Category GetByName(string name)
	{
		return db.Categories.SingleOrDefault(c => c.Name == name);
	}

	public async Task<int> InsertAsync(Category newCategory, List<IFormFile> Image)
	{
		foreach (var item in Image)
		{
			if (item.Length > 0)
			{
				using var stream = new MemoryStream();
				await item.CopyToAsync(stream);
				newCategory.Image = stream.ToArray();

			}
		}
		db.Categories.Add(newCategory);
		int raws = db.SaveChanges();
		return raws;
	}

	public async Task<int> UpdateAsync(Category editCategory, List<IFormFile> Image)
	{
		var category = db.Categories.SingleOrDefault(c => c.Id == editCategory.Id);
		foreach (var item in Image)
		{
			if (item.Length > 0)
			{
				using var stream = new MemoryStream();
				await item.CopyToAsync(stream);
				editCategory.Image = stream.ToArray();
			}
		}

		category!.Name = editCategory.Name;
		if (Image.Count > 0)
		{
			category.Image = editCategory.Image;
		}
		category.Description = editCategory.Description;

		int raws = db.SaveChanges();
		return raws;
	}
}
