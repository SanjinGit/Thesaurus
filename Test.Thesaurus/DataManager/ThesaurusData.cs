using DataAccess.Thesaurus.Entities;
using System;
using System.Collections.Generic;

namespace Test.Thesaurus.DataManager
{
    public static class ThesaurusData
    {
        public static List<Word> GetWords(int numberOfWords = 100)
        {
            var words = new List<Word>();

            for (int i = 1; i <= numberOfWords; i++)
            {
                words.Add(new Word()
                {
                    Id = i,
                    Name = new Guid().ToString(),
                    Synonyms = new List<Synonym>()
                    {
                        new Synonym()
                        {
                            WordId = i,
                            Id = i,
                            SynonymName =  new Guid().ToString()
                        },
                        new Synonym()
                        {
                            WordId = i,
                            Id = i,
                            SynonymName =  new Guid().ToString()
                        }
                    }
                });
            }

            return words;
        }

        public static Word GetWordEntity()
        {
            var word = new Word()
            {
                Id = 1,
                Name = "Test word",
                Synonyms = new List<Synonym>()
                    {
                        new Synonym()
                        {
                            WordId = 1,
                            Id = 1,
                            SynonymName =  new Guid().ToString()
                        },
                        new Synonym()
                        {
                            WordId = 1,
                            Id = 2,
                            SynonymName =  new Guid().ToString()
                        }
                    }
            };

            return word;
        }
    }
}
