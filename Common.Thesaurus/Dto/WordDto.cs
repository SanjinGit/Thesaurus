using System.ComponentModel.DataAnnotations;

namespace Common.Thesaurus.Dto
{
    /// <summary>
    /// Word data transfer object
    /// </summary>
    public class WordDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Word")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Synonyms (comma separated)")]
        public string Synonyms { get; set; }
    }
}
