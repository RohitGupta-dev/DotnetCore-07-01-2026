using Repository_Pattern.Models;
using System.Linq.Expressions;

namespace Repository_Pattern.Repo
{
    public interface ICollegeRepository<T>
    {
        Task<List<T>> GetAll();
        Task<T> GetById(Expression<Func<T, bool>> filter, bool useAsNoTracking);
        Task<T> GetByName(Expression<Func<T, bool>> filter);
        Task<T> Create(T dbReacord);
        Task<T> Update(T dbReacord);
        Task<bool> Delete(T dbReacord);
    }
}
