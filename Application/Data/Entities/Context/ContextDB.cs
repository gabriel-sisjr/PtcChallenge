using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Data.Entities.Context
{
    public class ContextDB : DbContext
    {
        public ContextDB(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }
    }
}
