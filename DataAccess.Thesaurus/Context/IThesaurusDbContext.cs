using DataAccess.Thesaurus.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Threading.Tasks;

namespace DataAccess.Thesaurus.Context
{
    public interface IThesaurusDbContext
    {
        #region Database DbSets
        DbSet<Word> Words { get; set; }

        DbSet<Synonym> Synonyms { get; set; }

        DatabaseFacade Database { get; }

        #endregion

        int SaveChanges();

        Task<int> SaveChangesAsync();

        DbSet<TEntity> Set<TEntity>() where TEntity : class;
    }
}
