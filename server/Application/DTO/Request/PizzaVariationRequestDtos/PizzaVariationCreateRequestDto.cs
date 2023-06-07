namespace Application.DTO.Request
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Application.Interfaces;

    public class PizzaVariationCreateRequestDto : IPizzaVariationWithBothIngredientsTypes
    {
        [Required]
        public string PizzaId { get; set; }

        [Required]
        public string SizeId { get; set; }

        [Required]
        public string DoughId { get; set; }

        [Required]
        public IEnumerable<string> AdditionalIngredients { get; set; }

        [Required]
        public IEnumerable<string> Ingredients { get; set; }
    }
}
