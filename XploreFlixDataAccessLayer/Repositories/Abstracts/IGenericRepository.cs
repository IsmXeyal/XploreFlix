namespace XploreFlixDataAccessLayer.Repositories.Abstracts;

public interface IGenericRepository<T> where T : new()
{
	int Delete(int id);
	ICollection<T>? GetAll();
}
