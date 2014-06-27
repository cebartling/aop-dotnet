using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UnityAopSpike.Core.Domain
{
    public class Order
    {
        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("order_number", Order = 2)]
        public string OrderNumber { get; set; }

        [Column("order_datetime", Order = 3)]
        public DateTime OrderDateTime { get; set; }
    }
}