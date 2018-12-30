using Microsoft.EntityFrameworkCore;

namespace WebApplication2.DataAccess
{
    public class DataAccessContext : DbContext
    {
        public DataAccessContext(DbContextOptions<DataAccessContext> options)
            : base(options)
        {
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
//            optionsBuilder.UseSqlServer();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.AddEntitiesFromAssembly(GetType().Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}