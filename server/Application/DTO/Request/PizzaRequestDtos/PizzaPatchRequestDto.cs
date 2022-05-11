namespace Application.DTO.Request
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Application.Interfaces;

    public class PizzaPatchRequestDto : INamedPizzaWithIngredients
    {
        [StringLength(20, MinimumLength = 1)]
        public string Name { get; set; }

        [StringLength(150, MinimumLength = 10)]
        public string Description { get; set; }

        public string ImageLink { get; set; }

        public string SingleItemImageLink { get; set; }

        public IEnumerable<string> Ingredients { get; set; }

        public int Discount { get; set; }

        public int BonusCoef { get; set; }
    }
}
