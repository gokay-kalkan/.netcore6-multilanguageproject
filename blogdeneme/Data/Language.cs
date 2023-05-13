using System.ComponentModel.DataAnnotations;

namespace blogdeneme.Data
{
    public class Language
    {
        [Key]
        public int Id { get; set; }
        public string LanguageName { get; set; }

        public virtual List<Blog> Blogs { get; set; }

    }
}
