using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UnityAopSpike.Core.Domain
{
    public class Product
    {
        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("name", Order = 2)]
        public string Name { get; set; }

        [Column("description", Order = 3)]
        public string Description { get; set; }
    }
}