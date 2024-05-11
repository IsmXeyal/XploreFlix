using Microsoft.AspNetCore.Http;
using XploreFlixDomainLayer.Entities;

namespace XploreFlixDataAccessLayer.Repositories.Abstracts;

public interface ICinemaRepository : IGenericRepository<Cinema>
{
	Cinema GetById(int id);
	Cinema GetByLocation(string location);
	Cinema GetByName(string name);
	Task<int> InsertAsync(Cinema newCinema, List<IFormFile> Image);
	Task<int> UpdateAsync(Cinema EditCin, int id, List<IFormFile> Image);
}
