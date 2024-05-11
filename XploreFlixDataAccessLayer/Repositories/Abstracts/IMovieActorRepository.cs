using XploreFlixDomainLayer.Entities;

namespace XploreFlixDataAccessLayer.Repositories.Abstracts;

public interface IMovieActorRepository
{
	public List<MovieActor> GetAll();
}
