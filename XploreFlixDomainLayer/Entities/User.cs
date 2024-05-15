using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace XploreFlixDomainLayer.Entities;

public class User : IdentityUser
{
	[Required]
	public string? FullName { get; set; }
	public byte[]? Image { get; set; }

	public virtual List<MovieOrder>? MovieOrders { get; set; }
	public virtual List<Card>? Cards { get; set; }
}
