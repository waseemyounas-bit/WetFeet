using DataAccess.Repository;

namespace DataAccess.UoW
{
    public interface IUnitofWork : IDisposable
    {
        IRepository<T> GetRepository<T>() where T : class;
        int saveChanges();
    }
}
