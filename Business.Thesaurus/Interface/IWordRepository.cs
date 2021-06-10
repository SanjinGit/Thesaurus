using Common.Thesaurus.Dto;
using DataAccess.Thesaurus.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Thesaurus.Interface
{
    /// <summary>
    /// Word Repository representation
    /// </summary>
    public interface IWordRepository
    {
        Task Create(Word word);

        Task<List<Word>> GetAll();

        Task<List<Word>> GetFiltered(string wordOption);

        Task<Word> Get(int id);

        Task Delete(Word word);

        Task<bool> Exists(int id);

        Task Update(WordDto word);
    }
}
