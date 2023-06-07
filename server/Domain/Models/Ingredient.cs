namespace Domain.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Ingredient : EntityBase
    {
        public Ingredient()
        {
            Pizzas = new List<Pizza>();
            PizzasVariations = new List<PizzaVariation>();
        }

        [Required]
        [StringLength(20, MinimumLength = 1)]
        public string Name { get; set; }

        [Required]
        [Range(0.1, 1000)]
        public decimal Price { get; set; }

        [Required]
        public string ImageLink { get; set; }

        [Required]
        public bool IsAvailable { get; set; }

        [Required]
        public bool IsObligatory { get; set; }

        [Required]
        public ICollection<Pizza> Pizzas { get; set; }

        [Required]
        public ICollection<PizzaVariation> PizzasVariations { get; set; }
    }
}
