using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using XploreFlixDomainLayer.Entities;

namespace XploreFlixDataAccessLayer.Contexts;

#nullable disable
public class XploreFlixDbContext : IdentityDbContext
{
	public XploreFlixDbContext() { }
	public XploreFlixDbContext(DbContextOptions options) : base(options) { }
	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.UseLazyLoadingProxies();
		base.OnConfiguring(optionsBuilder);
	}

	public virtual DbSet<User> Users { get; set; }
	public virtual DbSet<Movie> Movies { get; set; }
	public virtual DbSet<Cinema> Cinemas { get; set; }
	public virtual DbSet<Actor> Actors { get; set; }
	public virtual DbSet<Producer> Producers { get; set; }
	public virtual DbSet<Category> Categories { get; set; }
	public virtual DbSet<MovieActor> MovieActors { get; set; }
	public virtual DbSet<MovieInCinema> MovieInCinemas { get; set; }
	public virtual DbSet<Card> Card { get; set; }
	public virtual DbSet<MovieOrder> MovieOrders { get; set; }
}