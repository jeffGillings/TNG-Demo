namespace Microsoft.EntityFrameworkCore
{
    using WebAPI.Data.Domain;

    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comment>().HasData(
                new Comment { Id = 1, Statement = "Hello"},
                new Comment { Id = 2, Statement = "What's up?", Tone = "Polite"},
                new Comment { Id = 3, Statement = "Friends, Romans and country men"}
            );
        }
    }
}
