using System.Data.Entity;
using UnityAopSpike.Core.Domain;

namespace UnityAopSpike.DataAccess.Contexts
{
    public class CreateOrderDbContext : DbContext, ICreateOrderDbContext
    {
        public CreateOrderDbContext() : base("name=UnityAopSpike")
        {
        }

        public DbSet<Order> Orders { get; set; }
    }
}