using XploreFlixDataAccessLayer.Contexts;
using XploreFlixDataAccessLayer.Repositories.Abstracts;
using XploreFlixDomainLayer.Entities;

namespace XploreFlixDataAccessLayer.Repositories.Concretes;

public class MovieInCinemaRepository : IMovieInCinemaRepository
{
	internal readonly XploreFlixDbContext db;
	public MovieInCinemaRepository(XploreFlixDbContext db)
	{
		this.db = db;
	}

	public void Delete(int id)
	{
		var movie = db.MovieInCinemas.SingleOrDefault(w => w.Id == id);
		db.MovieInCinemas.Remove(movie!);
	}

	public List<MovieInCinema> GetAll()
	{
		var Movies = db.MovieInCinemas.ToList();
		return Movies;
	}

	public MovieInCinema GetById(int id)
	{
		var Movie = db.MovieInCinemas.SingleOrDefault(w => w.Id == id);
		return Movie!;
	}

	public void Insert(List<MovieInCinema> mic)
	{
		foreach (var item in mic)
		{
			db.MovieInCinemas.Add(item);
		}
		db.SaveChanges();
	}

	public void Update(int id, MovieInCinema mic)
	{
		var oldMovie = db.MovieInCinemas.SingleOrDefault(w => w.Id == id);
		oldMovie!.CinemaId = mic.CinemaId;
		oldMovie.MovieId = mic.MovieId;
		oldMovie.Quantity = mic.Quantity;
	}
}
