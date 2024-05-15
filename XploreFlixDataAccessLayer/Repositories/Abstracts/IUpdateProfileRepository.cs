using Microsoft.AspNetCore.Http;
using XploreFlixDomainLayer.Entities;

namespace XploreFlixDataAccessLayer.Repositories.Abstracts;

public interface IUpdateProfileRepository
{
	User GetById(string? id);
	Task<int> InsertAsync(User NewUser, List<IFormFile> Image);
	Task<int> UpdateAsync(string? id, User UpdateUser, List<IFormFile> Image);
}
