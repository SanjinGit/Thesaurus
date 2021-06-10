using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Thesaurus.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Thesaurus.Context
{
    /// <summary>
    /// DbContext implementation.
    /// </summary>
    public class ThesaurusDbContext : DbContext, IThesaurusDbContext
    {
        public ThesaurusDbContext()
        {
        }

        public ThesaurusDbContext(DbContextOptions<ThesaurusDbContext> options)
            : base(options)
        {
            LoadDataForInMemoryDatabase();
        }

        /// <summary>
        /// Database seeder.
        /// </summary>
        public void LoadDataForInMemoryDatabase()
        {
            if (Words.Any())
            {
                return;
            }

            List<Word> words = new List<Word>()
            {
                new Word()
                {
                    Name = "Car",
                    Synonyms = new List<Synonym>()
                    {
                        new Synonym()
                        {
                            SynonymName = "ç1"
                        },
                        new Synonym()
                        {
                            SynonymName = "c2"
                        }

                     },
                },
                new Word()
                {
                    Name = "Paper",
                    Synonyms = new List<Synonym>()
                    {
                        new Synonym()
                        {
                            SynonymName = "p1"
                        },
                        new Synonym()
                        {
                            SynonymName = "p2"
                        }

                     },
                }
            };

            Words.AddRange(words);

            base.SaveChanges();
        }

        #region DbSets

        public virtual DbSet<Word> Words { get; set; }
        public virtual DbSet<Synonym> Synonyms { get; set; }

        #endregion

        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                throw new Exception("DbConnection is not configured correctly.");
            }
        }
    }
}
