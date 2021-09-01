namespace Domain.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class PizzaVariation : EntityBase
    {
        public PizzaVariation()
        {
            OrderLines = new List<OrderLine>();
            Ingredients = new List<Ingredient>();
            AdditionalIngredients = new List<AdditionalIngredient>();
        }

        [Required]
        public Pizza Pizza { get; set; }

        [Required]
        public Size Size { get; set; }

        [Required]
        public Dough Dough { get; set; }

        [Required]
        [Range(0, 10000)]
        public decimal Price { get; set; }

        [Required]
        public ICollection<OrderLine> OrderLines { get; set; }

        [Required]
        public ICollection<Ingredient> Ingredients { get; set; }

        [Required]
        public ICollection<AdditionalIngredient> AdditionalIngredients { get; set; }
    }
}
