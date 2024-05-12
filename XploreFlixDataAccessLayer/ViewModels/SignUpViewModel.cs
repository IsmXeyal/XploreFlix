using System.ComponentModel.DataAnnotations;

namespace XploreFlixDataAccessLayer.ViewModels;

public class SignUpViewModel
{
	[Required(ErrorMessage = "FullName is rquired")]
	[Display(Name = "FullName")]
	public string? FullName { get; set; }

	[Display(Name = "UserName")]
	[Required(ErrorMessage = "UserName is required")]
	public string? UserName { get; set; }
	[Display(Name = "Email Address")]
	[Required(ErrorMessage = "Email is required")]
	public string? Email { get; set; }
	[DataType(DataType.Password)]
	public string? Password { get; set; }
	[Display(Name = "Confirm Password")]
	[DataType(DataType.Password)]
	[Compare("Password", ErrorMessage = "Password does not match")]
	public string? ConfirmPassword { get; set; }
}
