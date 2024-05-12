using System.ComponentModel.DataAnnotations;

namespace XploreFlixDomainLayer.Entities;

public class User
{
	public int Id { get; set; }
	[Required]
	public string? FullName { get; set; }
	[Required]
	public string? UserName { get; set; }
	public string? Email { get; set; }
	public string? UserRole { get; set; }
	public string? Password { get; set; }
	public byte[]? Image { get; set; }

	public virtual List<MovieOrder>? MovieOrders { get; set; }
	public virtual List<Card>? Cards { get; set; }
}
