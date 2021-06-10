using AutoMapper;
using Common.Thesaurus.Dto;
using DataAccess.Thesaurus.Entities;
using System.Linq;

namespace Business.Thesaurus.Mapping
{
    public class WordProfile : Profile
    {
        /// <summary>
        /// Word entity and mapping profile
        /// </summary>
        public WordProfile()
        {
            CreateMap<Word, WordDto>()
                .ForMember(t => t.Synonyms,
                    opt => opt.MapFrom(t =>
                    string.Join(",", t.Synonyms.Select(s => s.SynonymName)
                                               .Select(tag => tag.Trim())
                                               .Where(tag => !string.IsNullOrEmpty(tag))
                                               .ToList())));
            CreateMap<WordDto, Word>()
                .ForMember(t => t.Synonyms,
                    opt => opt.MapFrom(t =>
                    t.Synonyms.Split(",", System.StringSplitOptions.None)
                              .Select(tag => tag.Trim())
                              .Where(tag => !string.IsNullOrEmpty(tag))
                              .ToList()
                              .Select(synonym => new Synonym()
                              {
                                  SynonymName = synonym
                              }).ToList()));
        }
    }
}
