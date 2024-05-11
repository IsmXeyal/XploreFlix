using Microsoft.AspNetCore.Http;
using XploreFlixDomainLayer.Entities;

namespace XploreFlixDataAccessLayer.Repositories.Abstracts;

public interface ICategoryRepository : IGenericRepository<Category>
{
	Category GetById(int id);
	Category GetByName(string name);
	Task<int> InsertAsync(Category newCinema, List<IFormFile> Image);
	Task<int> UpdateAsync(Category editMovie, List<IFormFile> Image);
}
