using System.Data.Entity;
using UnityAopSpike.Core.Domain;

namespace UnityAopSpike.DataAccess.Contexts
{
    public class CreateOrderDbContext : DbContext, ICreateOrderDbContext
    {
        public DbSet<Order> Orders { get; set; }
    }
}