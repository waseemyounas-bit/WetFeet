using System.Linq.Expressions;

namespace DataAccess.Repository
{
    public interface IRepository<T> where T : class
    {
        void Add(T entity);
		void AddRange(List<T> entities);
		void Delete(Guid id);
        void Delete(List<T> Entities);
        IQueryable<T> GetAll();
        T GetById(Guid id);
		T GetById(string id);
		void Update(T entity);
		public List<T> GetDataFiltered(Expression<Func<T, bool>> condition);

	}
}
