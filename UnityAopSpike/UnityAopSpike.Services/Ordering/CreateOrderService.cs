using System;
using UnityAopSpike.Core.Domain;
using UnityAopSpike.DataAccess.Contexts;

namespace UnityAopSpike.Services.Ordering
{
    public class CreateOrderService
    {
        public ICreateOrderDbContext _createOrderDbContext;

        public CreateOrderService(ICreateOrderDbContext createOrderDbContext)
        {
            _createOrderDbContext = createOrderDbContext;
        }

        public void Execute()
        {
            var order = new Order {OrderNumber = GenerateOrderNumber(), 
                OrderTimestamp = new DateTime()};
            _createOrderDbContext.Orders.Add(order);
            _createOrderDbContext.SaveChanges();
        }

        private string GenerateOrderNumber()
        {
            return "37589273489723";
        }
    }
}