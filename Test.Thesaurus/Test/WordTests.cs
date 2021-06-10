using Business.Thesaurus.Interface;
using Business.Thesaurus.Mapping.Configuration;
using Moq;
using Service.Thesaurus.Service;
using System.Linq;
using System.Threading.Tasks;
using Test.Thesaurus.DataManager;
using Xunit;

namespace Test.Thesaurus.Test
{
    public class WordTests
    {
        [Fact]
        public async Task TestWordGet()
        {
            var wordRepositoryMock = new Mock<IWordRepository>();

            wordRepositoryMock.Setup(x => x.Get(1)).ReturnsAsync(ThesaurusData.GetWordEntity());

            var wordService = new WordService(wordRepositoryMock.Object, MappingConfiguration.Get());
            var wordDto = await wordService.Get(id: 1);
            var fetchedSynonymsCount = wordDto.Synonyms.Split(",").Count();

            Assert.Equal(ThesaurusData.GetWordEntity().Id, wordDto.Id);
            Assert.Equal(ThesaurusData.GetWordEntity().Name, wordDto.Name);
            Assert.Equal(ThesaurusData.GetWordEntity().Synonyms.Count, fetchedSynonymsCount);
        }
    }
}
