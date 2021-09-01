namespace Domain.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Order : EntityBase
    {
        public Order()
        {
            OrderLines = new List<OrderLine>();
        }

        [Required]
        public ICollection<OrderLine> OrderLines { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public OrderStatus OrderStatus { get; set; }
    }
}
