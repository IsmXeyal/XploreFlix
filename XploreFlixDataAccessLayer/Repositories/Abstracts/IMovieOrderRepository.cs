using XploreFlixDomainLayer.Entities;

namespace XploreFlixDataAccessLayer.Repositories.Abstracts;

public interface IMovieOrderRepository
{
	public void Insert(MovieOrder order);
}
