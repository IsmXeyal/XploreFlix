using Microsoft.AspNetCore.Http;
using XploreFlixDomainLayer.Entities;

namespace XploreFlixDataAccessLayer.Repositories.Abstracts;

public interface IUpdateProfileRepository
{
	User GetById(int id);
	Task<int> InsertAsync(User NewUser, List<IFormFile> Image);
	Task<int> UpdateAsync(int id, User UpdateUser, List<IFormFile> Image);
}
