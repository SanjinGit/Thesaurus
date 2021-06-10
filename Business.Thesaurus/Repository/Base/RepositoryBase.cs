using Common.Thesaurus.Interfaces.Repository;
using DataAccess.Thesaurus.Context;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Business.Thesaurus.Repository.Base
{
    /// <summary>
    /// Abstract repository class which all model repositories can use to communicate with database.
    /// Only certain DbContext methods are exposed which are described in IRepositoryBase interface and implemented here.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected IThesaurusDbContext DbContext { get; set; }

        public RepositoryBase(IThesaurusDbContext ctx)
        {
            this.DbContext = ctx;
        }

        public IQueryable<T> FindAll()
        {
            return this.DbContext.Set<T>();
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return this.DbContext.Set<T>().Where(expression);
        }

        public void Create(params T[] entities)
        {
            this.DbContext.Set<T>().AddRange(entities);
        }

        public void Update(params T[] entities)
        {
            this.DbContext.Set<T>().UpdateRange(entities);
        }

        public void Delete(params T[] entities)
        {
            this.DbContext.Set<T>().RemoveRange(entities);
        }

        public int SaveChanges()
        {
            return this.DbContext.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await this.DbContext.SaveChangesAsync();
        }
    }
}
