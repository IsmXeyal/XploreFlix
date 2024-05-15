using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using XploreFlixDataAccessLayer.Contexts;
using XploreFlixDataAccessLayer.Repositories.Abstracts;
using XploreFlixDataAccessLayer.ViewModels;
using XploreFlixDomainLayer.Entities;

namespace XploreFlix.Controllers;

public class MovieController : Controller
{
    private readonly XploreFlixDbContext db;
    IMovieRepository movieRepo;
    private readonly ICategoryRepository categoryRepo;
    private readonly ICinemaRepository cinemaRepo;
    private readonly IProducerRepository produerService;
    private readonly IActorRepository actorService;
    private readonly IMovieActorRepository movieactorService;
    private readonly IMovieInCinemaRepository movieincinemaService;
    private readonly ICardRepository cartservice;
    #region Constructor Injection
    public MovieController(XploreFlixDbContext db, IMovieRepository movieRepo,
        ICategoryRepository categoryRepo, ICinemaRepository cinemaRepo,
        IProducerRepository produerService, IActorRepository actorService,
        IMovieActorRepository movieactorService, IMovieInCinemaRepository movieincinemaService, ICardRepository cartservice)
    {
        this.db = db;
        this.movieRepo = movieRepo;
        this.categoryRepo = categoryRepo;
        this.cinemaRepo = cinemaRepo;
        this.produerService = produerService;
        this.actorService = actorService;
        this.movieactorService = movieactorService;
        this.movieincinemaService = movieincinemaService;
        this.cartservice = cartservice;
    }
    #endregion
    static Guid iid;


    #region User
    #region Index
    public ActionResult Index()
    {
        MovieItemViewModel mivm = new()
        {
            Movies = movieRepo.GetAll(),
            Producers = (List<Producer>)produerService.GetAll()!,
            Cinemas = (List<Cinema>)cinemaRepo.GetAll()!,
            Actors = (List<Actor>)actorService.GetAll()!,
            Categories = (List<Category>)categoryRepo.GetAll()!,
            MovieActors = movieactorService.GetAll()

        };

        return View("IndexUser", mivm);
    }
    #endregion
    #region Details
    public ActionResult Details(Guid id)
    {
        Card card = new();
		card.UserId = HttpContext.Session.GetString("id");
		card.MovieId = id;

        MovieDetailsViewModel mdvm = new()
        {
            UserId = HttpContext.Session.GetString("id"),
            Movie = movieRepo.GetById(id),
            MovieActors = movieactorService.GetAll().Where(w => w.MovieId == id).ToList(),
            MoviesInCinemas = movieincinemaService.GetAll().Where(w => w.MovieId == id).ToList(),
            cards = cartservice.GetData(card),
        };

        return View("DetailsUser", mdvm);
    }
    #endregion
    #endregion
    #region Admin
    #region Index
    [Authorize(Roles = "Admin")]
    public ActionResult GetMoviesAdmin()
    {
        List<Movie> MovieView = movieRepo.GetAll();

        return View("AdminMovie", MovieView);
    }
    #endregion
    #region Details
    [Authorize(Roles = "Admin")]
    public ActionResult GetMoviesDetailsAdmin(Guid id)
    {
        MovieViewModel Moviemodel = movieRepo.GetMovieByIdAdmin(id);

        ViewBag.Cinemas = new SelectList(db.Cinemas.ToList(), "Id", "Name");
        ViewBag.Categories = new SelectList(db.Categories.ToList(), "Id", "Name");
        ViewBag.Actors = new SelectList(db.Actors.ToList(), "Id", "Name");
        ViewBag.Producers = new SelectList(db.Producers.ToList(), "Id", "Name");
        return View("MovieDetailsAdmain", Moviemodel);
    }
    #endregion
    #region Insert
    #region Get
    [Authorize(Roles = "Admin")]
    public IActionResult Create()
    {
        ViewBag.Cinemas = new SelectList(db.Cinemas.ToList(), "Id", "Name");
        ViewBag.Categories = new SelectList(db.Categories.ToList(), "Id", "Name");
        ViewBag.Actors = new SelectList(db.Actors.ToList(), "Id", "Name");
        ViewBag.Producers = new SelectList(db.Producers.ToList(), "Id", "Name");

        return View(new MovieViewModel());
    }
    #endregion
    #region Post
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(MovieViewModel movievm, List<IFormFile> Image)
    {
        if (ModelState.IsValid)
        {
            movieRepo.InsertAsync(movievm, Image);
            return RedirectToAction("GetMoviesAdmin", "Movie");
        }

        ViewBag.Cinemas = new SelectList(db.Cinemas.ToList(), "Id", "Name");
        ViewBag.Categories = new SelectList(db.Categories.ToList(), "Id", "Name");
        ViewBag.Actors = new SelectList(db.Actors.ToList(), "Id", "Name");
        ViewBag.Producers = new SelectList(db.Producers.ToList(), "Id", "Name");
        return View(movievm);
    }
    #endregion
    #endregion
    #region Update
    #region Get
    [Authorize(Roles = "Admin")]
    public ActionResult EditMovieFromAdmin(Guid id)
    {
        iid = id;
        MovieViewModel Moviemodel = movieRepo.GetMovieByIdAdmin(id);

        ViewBag.Cinemas = new SelectList(db.Cinemas.ToList(), "Id", "Name");
        ViewBag.Categories = new SelectList(db.Categories.ToList(), "Id", "Name");
        ViewBag.Actors = new SelectList(db.Actors.ToList(), "Id", "Name");
        ViewBag.Producers = new SelectList(db.Producers.ToList(), "Id", "Name");

        return View("Edit", Moviemodel);
    }
    #endregion
    #region Post
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public ActionResult Edit(MovieViewModel editMovie, List<IFormFile> Image)
    {

        Task<int> numOfRowsUpdated = movieRepo.UpdateAsync(editMovie, iid, Image);
        return RedirectToAction("Getmoviesadmin");
    }
    #endregion
    #endregion
    #region Delete
    public ActionResult Delete(Guid id)
    {
        movieRepo.Delete(id);
        return RedirectToAction("Getmoviesadmin");

    }

    #endregion
    #endregion
    #region Search
    #region Filter
    [HttpGet]
    public async Task<IActionResult> filterSearch(string? MovieName)
    {
        ViewData["MovieName"] = MovieName;

        if (!string.IsNullOrEmpty(MovieName))
        {
            var movies = new MovieItemViewModel()
            {
                Movies = db.Movies.Where(c => c.Name!.Contains(MovieName)).ToList(),
                Producers = (List<Producer>)produerService.GetAll()!,
                Cinemas = (List<Cinema>)cinemaRepo.GetAll()!,
                Actors = (List<Actor>)actorService.GetAll()!,
                Categories = (List<Category>)categoryRepo.GetAll()!,
                MovieActors = movieactorService.GetAll()
            };

            //movie = movie;
            return View("IndexUser", movies);
        }
        return Content("nothing");
    }
    #endregion
    #region By Name
    [HttpGet]
    public async Task<IActionResult> movieSearch(string Keyword)
    {
        ViewData["searching"] = Keyword;
        var movies = db.Movies.Select(x => x);
        if (!string.IsNullOrEmpty(Keyword))
        {
            movies = movies.Where(c => c.Name.Contains(Keyword));

        }
        return View(await movies.AsNoTracking().ToListAsync());
    }

    #endregion
    #region Another Search
    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetMoviesAdmin(string Keyword)
    {
        ViewData["searching"] = Keyword;
        var movies = db.Movies.Select(x => x);
        if (!string.IsNullOrEmpty(Keyword))
        {
            movies = movies.Where(c => c.Name.Contains(Keyword));

        }
        return View("AdminMovie", await movies.AsNoTracking().ToListAsync());
    }
    #endregion
    #region Add Cinema
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public IActionResult AddCinema(List<string> cinemas)
    {
        var cinemaNames = new List<string>();
        foreach (string id in cinemas)
        {
            cinemaNames.Add(cinemaRepo.GetById(int.Parse(id)).Name!);
        }
        ViewBag.Cinemas = cinemaNames;
        return PartialView("_AddCinema", new MovieViewModel());
    }
    #endregion
    #endregion
}
