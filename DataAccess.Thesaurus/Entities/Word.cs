using System.Collections.Generic;

namespace DataAccess.Thesaurus.Entities
{
    public class Word
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual List<Synonym> Synonyms { get; set; }
    }
}
