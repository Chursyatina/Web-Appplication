namespace Domain.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class OrderStatus : EntityBase
    {
        public OrderStatus()
        {
            Orders = new List<Order>();
        }

        [Required]
        public string Name { get; set; }

        [Required]
        public ICollection<Order> Orders { get; set; }
    }
}
