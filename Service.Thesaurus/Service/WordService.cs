using AutoMapper;
using Business.Thesaurus.Interface;
using Common.Thesaurus.Dto;
using Common.Thesaurus.Interfaces.Service;
using DataAccess.Thesaurus.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Thesaurus.Service
{
    /// <summary>
    /// Word service class which is used to communicate with repositoy layer.
    /// It is used to map data (entities to dtos and vice versa).
    /// It also can be used to validate incoming data before accessing repository layer.
    /// Depencency injection is done using class constructor (in this and all other classes).
    /// </summary>
    public class WordService : IWordService
    {
        private readonly IWordRepository _wordRepository;
        private readonly IMapper _mapper;

        public WordService(IWordRepository wordRepository, IMapper mapper)
        {
            _wordRepository = wordRepository;
            _mapper = mapper;
        }

        public async Task Update(WordDto wordDto)
        {
            await _wordRepository.Update(wordDto);
        }

        public async Task Create(WordDto wordDto)
        {
            var wordEntity = _mapper.Map<Word>(wordDto);
            await _wordRepository.Create(wordEntity);
        }

        public async Task<WordDto> Get(int id)
        {
            var word = await _wordRepository.Get(id: id);
            return _mapper.Map<WordDto>(word);
        }

        public async Task<List<WordDto>> GetAll()
        {
            var words = await _wordRepository.GetAll();
            return _mapper.Map<List<WordDto>>(words);
        }

        public async Task<List<WordDto>> GetFiltered(string wordOption)
        {
            var words = await _wordRepository.GetFiltered(wordOption: wordOption);
            return _mapper.Map<List<WordDto>>(words);
        }

        public async Task Delete(int id)
        {
            var word = await _wordRepository.Get(id: id);
            await _wordRepository.Delete(word);
        }

        public async Task<bool> Exists(int id)
        {
            return await _wordRepository.Exists(id: id);
        }
    }
}
