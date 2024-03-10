using Data.Context;
using DataAccess.Repository;

namespace DataAccess.UoW
{
    public class UnitofWork : IUnitofWork
    {
        private DataContext dataContext;
        public UnitofWork(DataContext _dataContext)
        {
            dataContext = _dataContext;
        }
        public void Dispose()
        {
            dataContext.Dispose();
        }

        public IRepository<T> GetRepository<T>() where T : class
        {
            return new Repository<T>(dataContext);
        }

        public int saveChanges()
        {
            return dataContext.SaveChanges();
        }
    }
}
