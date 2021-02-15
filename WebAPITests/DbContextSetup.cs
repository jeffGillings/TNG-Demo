namespace WebAPITests
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using WebAPI.Data;

    public class DbContextSetup : IDisposable
    {
        public ApplicationDbContext DbContext { get; private set;}
        public DbContextSetup()
        {
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            builder.UseInMemoryDatabase(databaseName: "CustomerEngagement");
            var options = builder.Options;
            DbContext = new ApplicationDbContext(options);
        }

        public void Dispose()
        {
            if(DbContext != null)
            {
                DbContext.Dispose();
            }
        }
    }
}
