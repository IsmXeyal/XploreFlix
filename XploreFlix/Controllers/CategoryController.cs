using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using XploreFlixDataAccessLayer.Contexts;
using XploreFlixDataAccessLayer.Repositories.Abstracts;
using XploreFlixDomainLayer.Entities;

namespace XploreFlix.Controllers;

public class CategoryController : Controller
{
    ICategoryRepository categoryRepo;
    XploreFlixDbContext db;
    #region Constructor Injection
    public CategoryController(ICategoryRepository _categoryRepo, XploreFlixDbContext _db)
    {
        categoryRepo = _categoryRepo;
        db = _db;
    }
    #endregion


    #region User
    #region Index
    public ActionResult Index()
    {
        List<Category> categories = (List<Category>)categoryRepo.GetAll()!;

        return View(categories);
    }
    #endregion
    #region Details
    public ActionResult Details(int id)
    {
        Category category = categoryRepo.GetById(id);
        return View("DetailsUser", category);
    }
    #endregion
    #endregion


    #region Admin
    #region Index
    [Authorize(Roles = "Admin")]
    public IActionResult AdminCategories()
    {
        List<Category> categories = (List<Category>)categoryRepo.GetAll()!;
        return View("AdminCategories", categories);
    }
    #endregion
    #region Details
    [Authorize(Roles = "Admin")]
    public ActionResult CategoriesDetailsAdmin(int id)
    {

        Category Categories = categoryRepo.GetById(id);
        return View("CategoriesDetailsAdmin", Categories);
    }
    #endregion
    #region Insert
    #region Get
    public IActionResult InsertCategoryForm()
    {
        return View("InsertCategoryForm", new Category());
    }
    #endregion
    #region Post
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public IActionResult Create(Category newCategory, List<IFormFile> Image)
    {
        if (ModelState.IsValid)
        {
            categoryRepo.InsertAsync(newCategory, Image);
            return RedirectToAction("AdminCategories");
        }

        return RedirectToAction("InsertCategoryForm");
        //Retrun to Details
    }
    #endregion

    #endregion
    #region Update
    #region Get
    public IActionResult UpdateCategoryForm(int id)
    {
        var category = categoryRepo.GetById(id);
        return View("UpdateCategoryForm", category);
    }
    #endregion
    #region Post
    public IActionResult UpdateCategory(Category EditCategory, List<IFormFile> Image)
    {
        if (ModelState.IsValid)
        {
            categoryRepo.UpdateAsync(EditCategory, Image);
            return RedirectToAction("AdminCategories");
        }
        return RedirectToAction("UpdateCategoryForm", EditCategory);
    }
    #endregion

    #endregion
    #region Delete
    public ActionResult Delete(int id)
    {
        int numOfRowsDeleted = categoryRepo.Delete(id);
        return RedirectToAction("AdminCategories");

    }
    #endregion
    #endregion
    #region Search
    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> AdminCategories(string Keyword)
    {
        ViewData["searching"] = Keyword;
        var categories = db.Categories.Select(x => x);
        if (!string.IsNullOrEmpty(Keyword))
        {
            categories = categories.Where(c => c.Name!.Contains(Keyword));

        }
        return View(await categories.AsNoTracking().ToListAsync());
    }
    #endregion
    #region GetById
    public ActionResult Category(int id)
    {

        Category category = categoryRepo.GetById(id);
        return View(category);
    }
    #endregion

    #region ForView
    public ActionResult Grid()
    {

        return PartialView("_Grid", categoryRepo.GetAll());
    }

    public ActionResult List()
    {

        return PartialView("_List", categoryRepo.GetAll());
    }
    #endregion
}
