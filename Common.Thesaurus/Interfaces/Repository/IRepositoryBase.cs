using System;
using System.Linq;
using System.Linq.Expressions;

namespace Common.Thesaurus.Interfaces.Repository
{
    /// <summary>
    /// Representation of repository methods.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepositoryBase<T>
    {
        IQueryable<T> FindAll();
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);
        void Create(params T[] entities);
        void Update(params T[] entities);
        void Delete(params T[] entities);
    }
}
