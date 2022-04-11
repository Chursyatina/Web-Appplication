namespace Domain.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Basket : EntityBase
    {
        public Basket()
        {
            OrderLines = new List<OrderLine>();
        }

        [Required]
        public User User { get; set; }

        [Required]
        public ICollection<OrderLine> OrderLines { get; set; }

        [Required]
        public decimal Price { get; set; }
    }
}
