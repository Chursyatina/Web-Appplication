namespace Application.DTO.Request
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Application.Interfaces;

    public class PizzaCreateRequestDto : INamedPizzaWithIngredients
    {
        [Required]
        [StringLength(20, MinimumLength = 1)]
        public string Name { get; set; }

        [Required]
        [StringLength(150, MinimumLength = 10)]
        public string Description { get; set; }

        [Required]
        [Range(0.1, 1)]
        public double Discount { get; set; }

        [Required]
        [Range(0.1, 1)]
        public double BonusCoef { get; set; }

        [Required]
        public string ImageLink { get; set; }

        [Required]
        public string SingleItemImageLink { get; set; }

        [Required]
        public IEnumerable<string> Ingredients { get; set; }
    }
}
