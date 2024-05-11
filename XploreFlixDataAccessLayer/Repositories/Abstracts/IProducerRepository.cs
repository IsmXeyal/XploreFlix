using Microsoft.AspNetCore.Http;
using XploreFlixDomainLayer.Entities;

namespace XploreFlixDataAccessLayer.Repositories.Abstracts;

public interface IProducerRepository : IGenericRepository<Producer>
{
	Guid id { get; set; }
	Producer GetById(int id);
	Producer GetByName(string name);
	Task<int> InsertAsync(Producer newProducer, List<IFormFile> Image);
	Task<int> UpdateAsync(Producer EditProducer, int id, List<IFormFile> Image);
}
