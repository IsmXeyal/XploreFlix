using Microsoft.AspNetCore.Http;
using XploreFlixDomainLayer.Entities;

namespace XploreFlixDataAccessLayer.Repositories.Abstracts;

public interface IActorRepository : IGenericRepository<Actor>
{
	Actor GetById(int id);
	Actor GetByName(string name);
	Task<int> InsertAsync(Actor newActor, List<IFormFile> Image);
	Task<int> UpdateAsync(Actor EditActor, int id, List<IFormFile> Image);
}
