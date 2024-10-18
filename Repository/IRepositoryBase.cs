using System.Linq.Expressions;

namespace Repository
{
	public interface IRepositoryBase<T>
	{
		IQueryable<T> FindAll();
		IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);
		T Create(T entity);
		void Delete(T entity);
		void Update(T entity);

	}
}
