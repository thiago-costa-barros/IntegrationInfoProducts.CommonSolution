using CommonSolution.Entities;
using Microsoft.EntityFrameworkCore;
using SharpCompress.Common;
using System.ComponentModel.DataAnnotations;
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
                    .Where(t => t.IsClass
                            && !t.IsAbstract
                            && t.GetCustomAttribute<TableAttribute>() != null);

                foreach (var type in entityTypes)
                {
                    var tableAttr = type.GetCustomAttribute<TableAttribute>();
                    var entity = modelBuilder.Entity(type);

                    if (tableAttr != null)
                    {
                        entity.ToTable(tableAttr.Name, tableAttr.Schema);
                    }
                    else
                    {
                        entity.ToTable(type.Name, "CoreSchema");
                    }
                }
            }
        }
    }
}
