using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using XploreFlixDataAccessLayer.Repositories.Abstracts;
using XploreFlixDomainLayer.Entities;

namespace XploreFlix.Controllers;

[Authorize]
public class CardController : Controller
{
    private readonly ICardRepository cartService;

    public CardController(ICardRepository cartService)
    {
        this.cartService = cartService;
    }
    public IActionResult Index(Card cart)
    {

        cart.UserId = HttpContext.Session.GetString("id");

        List<Card> carts = cartService.GetData(cart);
        return View(carts);
    }

    [Authorize]
    public JsonResult Insert(Movie product)
    {

        Card cart = new()
        {
            UserId = HttpContext.Session.GetString("id"),
            MovieId = product.Id,
        };
        var c = cartService.GetData(cart).ToList();

        if (c.Count == 0)
            cartService.Insert(cart);
        else
            cartService.Delete(cart);

        return Json(true);
    }
}
