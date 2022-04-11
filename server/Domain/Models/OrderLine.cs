namespace Domain.Models
{
    using System.ComponentModel.DataAnnotations;

    public class OrderLine : EntityBase
    {
        [Required]
        public PizzaVariation PizzaVariation { get; set; }

        public Order Order { get; set; }

        public Basket Basket { get; set; }

        [Required]
        [Range(0.1, 100000)]
        public decimal Price { get; set; }

        [Required]
        [Range(0, 10)]
        public int Quantity { get; set; }
    }
}
