namespace Application.DTO.Request
{
    using System.Collections.Generic;
    using Application.Interfaces;

    public class PizzaVariationPatchRequestDto : IPizzaVariationWithBothIngredientsTypes
    {
        public string PizzaId { get; set; }

        public string SizeId { get; set; }

        public string DoughId { get; set; }

        public IEnumerable<string> AdditionalIngredients { get; set; }

        public IEnumerable<string> Ingredients { get; set; }
    }
}
