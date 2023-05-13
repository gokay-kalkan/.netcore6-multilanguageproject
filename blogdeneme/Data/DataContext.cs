using Microsoft.EntityFrameworkCore;

namespace blogdeneme.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options):base(options)
        {

        }

        public DbSet<Blog> Blogs { get; set; }
    
        public DbSet<Language> Languages { get; set; }

   
    }
}
