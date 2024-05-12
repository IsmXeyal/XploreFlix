using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using XploreFlix.Models;
using XploreFlixDataAccessLayer.Contexts;
using XploreFlixDataAccessLayer.ViewModels;
using XploreFlixDomainLayer.Entities;

namespace XploreFlix.Controllers;

public class AccountController : Controller
{

	#region LogIn
	public IActionResult Login(User user)
	{
		return View();
	}



	#endregion

	#region SignUp
	public IActionResult SignUp()
	{
		return View(new SignUpViewModel());
	}

	

	public IActionResult SignUpCompleted()
	{
		return View();
	}

	#endregion
}
