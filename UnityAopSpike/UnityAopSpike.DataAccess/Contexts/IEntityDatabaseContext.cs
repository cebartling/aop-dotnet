using System.Data.Entity;
using UnityAopSpike.Core.Domain;

namespace UnityAopSpike.DataAccess.Contexts
{
    public interface IEntityDatabaseContext
    {
        DbSet<Order> Orders { get; set; }
        DbSet<Product> Products { get; set; }

        int SaveChanges();
    }
}