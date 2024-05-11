using XploreFlixDomainLayer.Entities;

namespace XploreFlix.Models.ViewModels;

public class MovieDetailsViewModel
{
	public virtual Movie? Movie { get; set; }
	public virtual int UserId { get; set; }
	public virtual List<Card>? cards { get; set; }
	public virtual List<MovieActor>? MovieActors { get; set; }
	public virtual List<MovieInCinema>? MoviesInCinemas { get; set; }
}
