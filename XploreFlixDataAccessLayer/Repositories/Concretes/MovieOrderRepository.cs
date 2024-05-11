using XploreFlixDataAccessLayer.Contexts;
using XploreFlixDataAccessLayer.Repositories.Abstracts;
using XploreFlixDomainLayer.Entities;

namespace XploreFlixDataAccessLayer.Repositories.Concretes;

public class MovieOrderRepository : IMovieOrderRepository
{
	internal readonly XploreFlixDbContext? db;
	public MovieOrderRepository(XploreFlixDbContext? _db)
	{
		db = _db;
	}

	public void Insert(MovieOrder order)
	{
		db!.MovieOrders.Add(order);
		db.SaveChanges();
	}
}
