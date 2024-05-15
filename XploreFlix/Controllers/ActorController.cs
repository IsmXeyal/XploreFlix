using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using XploreFlixDataAccessLayer.Contexts;
using XploreFlixDataAccessLayer.Repositories.Abstracts;
using XploreFlixDomainLayer.Entities;

namespace XploreFlix.Controllers;

public class ActorController : Controller
{
    IActorRepository actorRepository;
    XploreFlixDbContext db;
    public ActorController(IActorRepository ActRepo, XploreFlixDbContext _db)
    {
        actorRepository = ActRepo;
        db = _db;
    }

    //get all actors for users 
    #region User
    #region User Index
    public ActionResult Index()
    {
        List<Actor> Actors = (List<Actor>)actorRepository.GetAll()!;
        return View(Actors);
    }
    #endregion

    #region User Details
    public ActionResult Details(int id)
    {

        Actor Actors = actorRepository.GetById(id);
        return View("DetailsUser", Actors);
    }
    #endregion
    #endregion

    #region Admin
    #region  Index
    [Authorize(Roles = "Admin")]
    public IActionResult AdminActors()
    {
        List<Actor> Actors = (List<Actor>)actorRepository.GetAll()!;
        return View("AdminActors", Actors);
    }
    #endregion
    #region  Details
    [Authorize(Roles = "Admin")]
    public ActionResult ActorsDetailsAdmin(int id)
    {

        Actor Actors = actorRepository.GetById(id);
        return View("ActorsDetailsAdmin", Actors);
    }
    #endregion
    #region Insert
    #region Get
    public IActionResult InsertActorForm()
    {
        return View("InsertActor", new Actor());
    }
    #endregion
    #region post
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public ActionResult Create(Actor NewActor, List<IFormFile> Image)
    {
        if (ModelState.IsValid)
        {
            actorRepository.InsertAsync(NewActor, Image);
            return RedirectToAction("AdminActors");
        }

        return RedirectToAction("Actor");
    }
    #endregion

    #endregion
    #region Update
    #region Get
    public IActionResult UpdateActorForm(int id)
    {
        var actor = actorRepository.GetById(id);

        return View("UpdateActor", actor);
    }
    #endregion
    #region post
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit(Actor EditActor, int id, List<IFormFile> Image)
    {
        if (ModelState.IsValid)
        {
            actorRepository.UpdateAsync(EditActor, id, Image);
            return RedirectToAction("AdminActors", "Actor");
        }
        return View("UpdateActor");

    }
    #endregion
    #endregion
    #region Delete
    [Authorize(Roles = "Admin")]
    public IActionResult Delete(int id)
    {
        actorRepository.Delete(id);
        return RedirectToAction("AdminActors");
    }
    #endregion
    #endregion


    #region Search


    [HttpGet]
    [Authorize(Roles = "Admin")]

    public async Task<IActionResult> AdminActors(string Keyword)
    {
        ViewData["searching"] = Keyword;
        var actors = db.Actors.Select(x => x);
        if (!string.IsNullOrEmpty(Keyword))
        {
            actors = actors.Where(c => c.Name!.Contains(Keyword));

        }
        return View(await actors.AsNoTracking().ToListAsync());
    }

    #endregion

    #region GetByName
    public ActionResult ActorName(string name)
    {

        Actor Actors = actorRepository.GetByName(name);
        return View(Actors);
    }
    #endregion
}
