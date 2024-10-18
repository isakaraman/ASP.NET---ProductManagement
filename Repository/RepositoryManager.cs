using Repository.Products;

namespace Repository
{
	public class RepositoryManager : IRepositoryManager
	{
		private readonly RepositoryContext _repositoryContext;
		private readonly Lazy<IProductRepository> _productRepository;

        public RepositoryManager(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
			_productRepository = new Lazy<IProductRepository>(()=>new ProductRepository(repositoryContext));
        }
        public IProductRepository Product => _productRepository.Value;

		public void Save()
		{
			_repositoryContext.SaveChanges();
		}
	}
}
