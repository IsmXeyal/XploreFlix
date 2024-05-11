using XploreFlixDomainLayer.Entities;

namespace XploreFlixDataAccessLayer.Repositories.Abstracts;
public interface ICardRepository
{
	List<Card> GetData(Card card);
	void Insert(Card mic);
	void Delete(Card cart);
}
