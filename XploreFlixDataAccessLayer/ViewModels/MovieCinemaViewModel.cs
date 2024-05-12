using XploreFlixDomainLayer.Entities;

namespace XploreFlixDataAccessLayer.ViewModels;

public class MovieCinemaViewModel
{
	public virtual Cinema? Cinema { get; set; }
	public virtual List<MovieInCinema>? Movies { get; set; }
}
