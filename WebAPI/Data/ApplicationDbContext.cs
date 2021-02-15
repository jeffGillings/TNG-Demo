namespace WebAPI.Data
{
    using Microsoft.EntityFrameworkCore;
    using WebAPI.Data.Domain;
    using WebAPI.Data.Infrastructure;

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new CommentEntityTypeConfiguration());
            builder.Seed();
        }
    }
}
