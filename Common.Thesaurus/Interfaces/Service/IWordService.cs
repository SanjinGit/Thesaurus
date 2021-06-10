using Common.Thesaurus.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Common.Thesaurus.Interfaces.Service
{
    /// <summary>
    /// Representation of WordService
    /// </summary>
    public interface IWordService
    {
        Task Create(WordDto wordDto);

        Task<List<WordDto>> GetAll();

        Task<List<WordDto>> GetFiltered(string wordOption);

        Task<WordDto> Get(int id);

        Task Delete(int id);

        Task<bool> Exists(int id);

        Task Update(WordDto wordDto);
    }
}
