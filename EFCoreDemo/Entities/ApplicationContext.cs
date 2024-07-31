using Microsoft.EntityFrameworkCore;

namespace EFCoreDemo.Entities
{
    public class ApplicationContext: DbContext
    {
        public ApplicationContext() { }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        
        }

        public DbSet<Account> Accounts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.;Database=Test;User ID=sa;Password=123456;TrustServerCertificate=true");
        }
    }
}
