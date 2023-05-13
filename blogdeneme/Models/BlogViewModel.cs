namespace blogdeneme.Models
{
    public class BlogViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Slug { get; set; }
       
        public int LanguageId { get; set; }
    }
}
