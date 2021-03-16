using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }
        public DbSet<Photo> Photos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=ALIENWARE-R2-17;Database=TrashPanda;Trusted_Connection=True;");
        }
    }
}