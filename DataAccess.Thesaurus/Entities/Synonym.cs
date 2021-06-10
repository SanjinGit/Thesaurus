namespace DataAccess.Thesaurus.Entities
{
    public class Synonym
    {
        public int Id { get; set; }
        public int WordId { get; set; }
        public virtual Word Word { get; set; }
        public string SynonymName { get; set; }
    }
}
