using XploreFlixDataAccessLayer.Contexts;
using XploreFlixDataAccessLayer.Repositories.Abstracts;
using XploreFlixDomainLayer.Entities;

namespace XploreFlixDataAccessLayer.Repositories.Concretes;

public class UserRepository : IUserRepository
{
	internal readonly XploreFlixDbContext db;
	public UserRepository(XploreFlixDbContext db)
	{
		this.db = db;
	}
	public int Delete(int id)
	{
		User? delUser = db.Users.SingleOrDefault(c => c.Id == id);
		db.Users.Remove(delUser!);
		int raws = db.SaveChanges();
		return raws;
	}

	public ICollection<User>? GetAll()
	{
		var users = db.Users.ToList();
		return users;
	}

	public User GetById(int id)
	{
		return db.Users.SingleOrDefault(c => c.Id == id);
	}
}
