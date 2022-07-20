using Microsoft.EntityFrameworkCore;

namespace ResumeManager.Data
{
    public class OutDbContext:DbContext
    {
        public OutDbContext(DbContextOptions<OutDbContext> options) : base(options)
        {
        }
        public virtual DbSet<Gender> Genders { get; set; }
    }
}
