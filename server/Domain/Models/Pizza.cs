namespace Domain.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Pizzas")]
    public class Pizza : EntityBase
    {
        public Pizza()
        {
            Ingredients = new List<Ingredient>();
            PizzaVariations = new List<PizzaVariation>();
        }

        [Required]
        [StringLength(20, MinimumLength = 1)]
        public string Name { get; set; }

        [Required]
        [StringLength(150, MinimumLength = 10)]
        public string Description { get; set; }

        [Required]
        public string ImageLink { get; set; }

        [Required]
        public string SingleItemImageLink { get; set; }

        [Required]
        [Range(0.1, 1000)]
        public decimal Price { get; set; }

        [Required]
        public bool IsAvailable { get; set; }

        [Required]
        public ICollection<Ingredient> Ingredients { get; set; }

        [Required]
        public ICollection<PizzaVariation> PizzaVariations { get; set; }
    }
}
