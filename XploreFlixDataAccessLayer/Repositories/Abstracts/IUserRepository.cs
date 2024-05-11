using XploreFlixDomainLayer.Entities;

namespace XploreFlixDataAccessLayer.Repositories.Abstracts;

public interface IUserRepository : IGenericRepository<User>
{
	User GetById(int id);
}
