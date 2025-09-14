using CommonSolution.Entities;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace CommonSolution.CrossCutting
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            var entitiesAssembly = Assembly.GetAssembly(typeof(AssemblyReferenceEntity));

            if (entitiesAssembly != null)
            {
                var entityTypes = entitiesAssembly
                    .GetTypes()
                    .Where(t => t.IsClass && !t.IsAbstract);

                foreach (var type in entityTypes)
                {
                    var tableAttr = type.GetCustomAttribute<TableAttribute>();

                    if (tableAttr != null)
                    {
                        modelBuilder.Entity(type).ToTable(tableAttr.Name, tableAttr.Schema);
                    }
                    else
                    {
                        modelBuilder.Entity(type).ToTable(type.Name, "CoreSchema");
                    }
                }
            }
        }
    }
}
