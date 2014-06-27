using System.Data.Entity;
using UnityAopSpike.Core.Domain;

namespace UnityAopSpike.DataAccess.Contexts
{
    public class EntityDatabaseContext : DbContext, IEntityDatabaseContext
    {
        public EntityDatabaseContext() : base("name=UnityAopSpike")
        {
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}