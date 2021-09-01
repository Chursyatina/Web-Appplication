namespace Domain.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Dough : EntityBase
    {
        public Dough()
        {
            PizzaVariations = new List<PizzaVariation>();
        }

        [Required]
        [StringLength(20, MinimumLength = 1)]
        public string Name { get; set; }

        [Required]
        [Range(0.1, 3)]
        public decimal PriceMultiplier { get; set; }

        [Required]
        public ICollection<PizzaVariation> PizzaVariations { get; set; }
    }
}
