using System;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TMA.Configuration;

namespace TMA.Data.Common
{
    public class DataContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public DataContext(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_configuration.GetDbConnectionString());
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(x => x.GetTypes())
                .Where(x => (typeof(IEntity).IsAssignableFrom(x)))
                .Where(x => !x.IsInterface && !x.IsAbstract)
                .ToList();

            foreach (var entity in types)
            {
                var entityTypeBuilder = modelBuilder.Entity(entity);

                if (entity.GetProperty("Id", BindingFlags.Instance) != null)
                {
                    entityTypeBuilder.HasKey("Id");
                }
            }
        }
    }
}
