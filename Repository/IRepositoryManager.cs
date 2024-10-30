
using Repository.Products;

namespace Repository
{
	public interface IRepositoryManager
	{
		IProductRepository Product {  get; }

		Task SaveAsync();
	}
}
