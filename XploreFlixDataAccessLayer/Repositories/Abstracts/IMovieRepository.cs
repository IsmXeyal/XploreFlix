using Microsoft.AspNetCore.Http;
using XploreFlixDataAccessLayer.ViewModels;
using XploreFlixDomainLayer.Entities;

namespace XploreFlixDataAccessLayer.Repositories.Abstracts;

public interface IMovieRepository
{
	int Delete(Guid id);
	List<Movie> GetAll();
	MovieViewModel GetMovieByIdAdmin(Guid id);
	Movie GetById(Guid id);
	Movie GetByName(string name);
	Task<int> InsertAsync(MovieViewModel newCinema, List<IFormFile> Image);
	Task<int> UpdateAsync(MovieViewModel editMovie, Guid id, List<IFormFile> Image);
}
