using Business.Thesaurus.Interface;
using Business.Thesaurus.Repository.Base;
using Common.Thesaurus.Dto;
using DataAccess.Thesaurus.Context;
using DataAccess.Thesaurus.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business.Thesaurus.Repository
{
    public class WordRepository : RepositoryBase<Word>, IWordRepository
    {
        public WordRepository(IThesaurusDbContext context)
            : base(context)
        {
        }

        /// <summary>
        /// Create new word.
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        public async Task Create(Word word)
        {
            base.Create(word);
            await base.SaveChangesAsync();
        }

        /// <summary>
        /// Get all words.
        /// </summary>
        /// <returns></returns>
        public async Task<List<Word>> GetAll()
        {
            return await base.FindAll()
                             .Include(s => s.Synonyms)
                             .ToListAsync();
        }

        /// <summary>
        /// Get all filtered words.
        /// </summary>
        /// <param name="wordOption"></param>
        /// <returns></returns>
        public async Task<List<Word>> GetFiltered(string wordOption)
        {
            return await
                base.FindByCondition(x => 
                        x.Name.ToLower() == wordOption.ToLower() || 
                        x.Synonyms.Any(s => s.SynonymName.ToLower() == wordOption.ToLower()))
                    .Include(s => s.Synonyms)
                    .ToListAsync();
        }

        /// <summary>
        /// Get single word.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Word> Get(int id)
        {
            return await base.FindByCondition(x => x.Id == id)
                             .Include(s => s.Synonyms)
                             .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Delete single word.
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        public async Task Delete(Word word)
        {
            base.Delete(word);
            await base.SaveChangesAsync();
        }

        /// <summary>
        /// Check if word exists with provided id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> Exists(int id)
        {
            return await base.FindByCondition(x => x.Id == id)
                             .AnyAsync();
        }

        /// <summary>
        /// Update word and synonyms.
        /// </summary>
        /// <param name="wordDto"></param>
        /// <returns></returns>
        public async Task Update(WordDto wordDto)
        {
            var wordToUpdate = await Get(id: wordDto.Id);

            wordToUpdate.Name = wordDto.Name;

            if (wordDto.Synonyms == null)
            {
                RemovePreviousSynonyms(wordToUpdate);
            }
            else
            {
                var incomingSynonyms = wordDto.Synonyms.Split(",").ToHashSet();

                if (wordToUpdate.Synonyms.Any())
                {
                    RemoveMissingSynonyms(wordToUpdate, incomingSynonyms);

                }

                AddNewSynonyms(wordToUpdate, incomingSynonyms);
            }

            await base.SaveChangesAsync();
        }

        #region Private helpers

        private static void AddNewSynonyms(Word wordToUpdate, HashSet<string> incomingSynonyms)
        {
            var newSynonyms = incomingSynonyms.Except(wordToUpdate.Synonyms.Select(s => s.SynonymName).ToList())
                                                        .Select(synonymWord => new Synonym()
                                                        {
                                                            Word = wordToUpdate,
                                                            SynonymName = synonymWord
                                                        })
                                                        .ToHashSet();

            wordToUpdate.Synonyms.AddRange(newSynonyms);
        }

        private static void RemoveMissingSynonyms(Word wordToUpdate, HashSet<string> incomingSynonyms)
        {
            var synonymsToDelte = wordToUpdate.Synonyms
                                              .Where(x => !incomingSynonyms.Contains(x.SynonymName))
                                              .ToList();

            foreach (var synonymToDelte in synonymsToDelte)
            {
                wordToUpdate.Synonyms.Remove(synonymToDelte);
            }
        }

        private static void RemovePreviousSynonyms(Word wordToUpdate)
        {
            wordToUpdate.Synonyms = null;
        }

        #endregion
    }
}
