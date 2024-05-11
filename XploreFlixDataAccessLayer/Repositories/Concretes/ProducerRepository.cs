using Microsoft.AspNetCore.Http;
using XploreFlixDataAccessLayer.Contexts;
using XploreFlixDataAccessLayer.Repositories.Abstracts;
using XploreFlixDomainLayer.Entities;

namespace XploreFlixDataAccessLayer.Repositories.Concretes;

public class ProducerRepository : IProducerRepository
{
	public Guid id { set; get; }
	public ProducerRepository()
	{
		id = Guid.NewGuid();
	}

	internal readonly XploreFlixDbContext? db;
	public ProducerRepository(XploreFlixDbContext? db)
	{
		this.db = db;
	}

	public int Delete(int id)
	{
		Producer? DelPro = db.Producers.SingleOrDefault(n => n.Id == id);
		db.Producers.Remove(DelPro!);
		int raws = db.SaveChanges();
		return raws;
	}

	public ICollection<Producer>? GetAll()
	{
		var producers = db.Producers.ToList();
		return producers;
	}

	public Producer GetById(int id)
	{
		return db.Producers.SingleOrDefault(n => n.Id == id);
	}

	public Producer GetByName(string name)
	{
		return db.Producers.SingleOrDefault(n => n.Name == name);
	}

	public async Task<int> InsertAsync(Producer newProducer, List<IFormFile> Image)
	{
		foreach (var item in Image)
		{
			if (item.Length > 0)
			{
				using var stream = new MemoryStream();
				await item.CopyToAsync(stream);
				newProducer.Image = stream.ToArray();
			}
		}
		db.Producers.Add(newProducer);
		int raws = db.SaveChanges();
		return raws;
	}

	public async Task<int> UpdateAsync(Producer EditProducer, int id, List<IFormFile> Image)
	{
		var Producer = db.Producers.SingleOrDefault(n => n.Id == id);
		foreach (var item in Image)
		{
			if (item.Length > 0)
			{
				using var stream = new MemoryStream();
				await item.CopyToAsync(stream);
				EditProducer.Image = stream.ToArray();
			}
		}
		Producer!.Id = EditProducer.Id;
		Producer.Name = EditProducer.Name;
		if (Image.Count != 0)
			Producer.Image = EditProducer.Image;
		Producer.Bio = EditProducer.Bio;
		int raws = db.SaveChanges();
		return raws;
	}
}
