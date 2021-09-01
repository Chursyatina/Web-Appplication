namespace Application.DTO.Request
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Application.Interfaces;

    public class PizzaVariationUpdateRequestDto : IPizzaVariationWithBothIngredientsTypes
    {
        [Required]
        public int? PizzaId { get; set; }

        [Required]
        public int? SizeId { get; set; }

        [Required]
        public int? DoughId { get; set; }

        [Required]
        public IEnumerable<int> AdditionalIngredients { get; set; }

        [Required]
        public IEnumerable<int> Ingredients { get; set; }
    }
}
