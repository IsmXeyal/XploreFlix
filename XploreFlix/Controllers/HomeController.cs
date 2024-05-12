using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using XploreFlix.Models;
using XploreFlixDataAccessLayer.Repositories.Abstracts;
using XploreFlixDataAccessLayer.ViewModels;
using XploreFlixDomainLayer.Entities;

namespace XploreFlix.Controllers;

public class HomeController : Controller
{
	private readonly ILogger<HomeController> _logger;
	private readonly IActorRepository actorService;
	private readonly ICategoryRepository categoryService;
	private readonly ICinemaRepository cinemaService;
	private readonly IMovieRepository movieServie;

	public HomeController(ILogger<HomeController> logger, IActorRepository ActorService,
			ICategoryRepository CategoryService, ICinemaRepository CinemaService,
			IMovieRepository MovieServie)
	{
		_logger = logger;
		actorService = ActorService;
		categoryService = CategoryService;
		cinemaService = CinemaService;
		movieServie = MovieServie;
	}

	public IActionResult Index()
	{
		//string id = HttpContext.Session.GetString("id");
		HomeViewModel addHome = new HomeViewModel()
		{
			Cinemas = (List<Cinema>?)cinemaService.GetAll(),
			Movies = movieServie.GetAll(),
			Categories = (List<Category>?)categoryService.GetAll(),
			Actors = (List<Actor>?)actorService.GetAll()
		};
		return PartialView(addHome);
	}

	public IActionResult Privacy()
	{
		return View();
	}

	[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
	public IActionResult Error()
	{
		return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
	}

	[Authorize(Roles = "Admin")]
	public IActionResult Admin()
	{
		return View();
	}
}
