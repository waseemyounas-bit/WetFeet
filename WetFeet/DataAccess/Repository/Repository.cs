using Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DataContext dataContext;
        private readonly DbSet<T> table;
        public Repository(DataContext _dataContext)
        {
            dataContext = _dataContext;
            table = dataContext.Set<T>();
        }
        public void Add(T entity)
        {
            table.Add(entity);
        }

		public void AddRange(List<T> entities)
		{
            table.AddRange(entities);
		}

		public void Delete(Guid id)
        {
            T entity = table.Find(id);
            table.Remove(entity);
        }

        public void Delete(List<T> Entities)
        {
            table.RemoveRange(Entities);
        }

        public IQueryable<T> GetAll()
        {
            return table;
        }

        public T GetById(Guid id)
        {
            return table.Find(id);
        }

		public T GetById(string id)
		{
			return table.Find(id);
		}

		public void Update(T entity)
        {
            table.Attach(entity);
            dataContext.Entry(entity).State = EntityState.Modified;
        }

		public List<T> GetDataFiltered(Expression<Func<T, bool>> condition)
        {
            return table.Where(condition).ToList();
        }

        public IQueryable<T> GetIQuerableDataFiltered(Expression<Func<T, bool>> condition)
        {
            return table.Where(condition);
        }
    }
}
