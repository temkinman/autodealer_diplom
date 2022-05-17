using System.Linq;

namespace AutoDealer.Web.Core.DB.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        bool Create(T item);
        T GetById(int id);
        bool Update(T item);
        bool Delete(T item);
    }
}
