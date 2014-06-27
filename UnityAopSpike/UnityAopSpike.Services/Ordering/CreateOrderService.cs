using System;
using UnityAopSpike.Core.Domain;
using UnityAopSpike.DataAccess.Contexts;

namespace UnityAopSpike.Services.Ordering
{
    public class CreateOrderService
    {
        public IEntityDatabaseContext EntityDatabaseContext;

        public CreateOrderService(IEntityDatabaseContext entityDatabaseContext)
        {
            EntityDatabaseContext = entityDatabaseContext;
        }

        public void Execute()
        {
            var order = new Order {OrderNumber = GenerateOrderNumber(), 
                OrderDateTime = new DateTime()};
            EntityDatabaseContext.Orders.Add(order);
            EntityDatabaseContext.SaveChanges();
        }

        private string GenerateOrderNumber()
        {
            return "37589273489723";
        }
    }
}