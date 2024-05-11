using XploreFlixDataAccessLayer.Contexts;
using XploreFlixDataAccessLayer.Repositories.Abstracts;
using XploreFlixDomainLayer.Entities;

namespace XploreFlixDataAccessLayer.Repositories.Concretes;

public class MovieActorRepository : IMovieActorRepository
{
	internal readonly XploreFlixDbContext? db;
	public MovieActorRepository(XploreFlixDbContext? _db)
	{
		db = _db;
	}
	public List<MovieActor> GetAll()
	{
		var MovieActor = db!.MovieActors.ToList();
		return MovieActor;
	}
}
