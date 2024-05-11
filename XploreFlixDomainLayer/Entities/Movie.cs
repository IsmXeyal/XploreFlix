using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace XploreFlixDomainLayer.Entities;

public class Movie
{
    // We are preventing the identity property on the primary key.
	// In other words, we are disabling automatic increment till we want.

	[Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
	[Required]
	public Guid Id { get; set; }
	public string? Name { get; set; }
	public DateTime StartDate { get; set; }
	public DateTime EndDate { get; set; }
	public byte[]? Image { get; set; }
	public double Price { get; set; }
	public string? Description { get; set; }
	public string? Trailer { get; set; }
	public double Rate { get; set; }
	[ForeignKey("Category")]
	public int Cat_Id { get; set; }
	[ForeignKey("Producer")]
	public int Producer_Id { get; set; }
	public virtual Category? Category { get; set; }
	public virtual Producer? Producer { get; set; }
	public virtual List<MovieOrder>? MovieOrders { get; set; }
	public virtual List<MovieInCinema>? MoviesInCinema { get; set; }
	public virtual List<Card>? Cards { get; set; }
	public virtual List<MovieActor>? MovieActors { get; set; }

}
