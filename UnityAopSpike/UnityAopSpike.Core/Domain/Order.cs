using System;

namespace UnityAopSpike.Core.Domain
{
    public class Order
    {
        public Order()
        {
        }

        public int Id { get; set; }

        public string OrderNumber { get; set; }

        public DateTime OrderTimestamp { get; set; }
    }
}