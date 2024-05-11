using XploreFlixDataAccessLayer.Contexts;
using XploreFlixDataAccessLayer.Repositories.Abstracts;
using XploreFlixDomainLayer.Entities;

namespace XploreFlixDataAccessLayer.Repositories.Concretes;

public class CardRepository : ICardRepository
{
	internal readonly XploreFlixDbContext db;
	public CardRepository(XploreFlixDbContext db)
	{
		this.db = db;
	}

	public void Delete(Card cart)
	{
		var movie = db.Card.Where(w => w.UserId == cart.UserId).SingleOrDefault(w => w.MovieId == cart.MovieId);
		db.Card.Remove(movie!);
		db.SaveChanges();
	}

	public List<Card> GetData(Card cards)
	{
		var card = db.Card.Where(w => w.UserId == cards.UserId).Where(w => w.MovieId == cards.MovieId).ToList();
		return card!;
	}

	public List<Card> GetAll(Card cards)
	{
		var card = db.Card.Where(w => w.UserId == cards.UserId).ToList();
		return card!;
	}

	public void Insert(Card card)
	{
		db.Card.Add(card);
		db.SaveChanges();
	}
}
