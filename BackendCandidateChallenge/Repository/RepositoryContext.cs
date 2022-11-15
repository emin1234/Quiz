using Microsoft.EntityFrameworkCore;
using Repository.Model.Domain;

namespace Repository
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions options)
            : base(options)
        { }

        public DbSet<Quiz> Quiz { get; set; }

        public DbSet<Question> Question { get; set; }

        public DbSet<Answer> Answer { get; set; }
    }
}
