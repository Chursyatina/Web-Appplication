namespace Application.DTO.Response
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Application.Interfaces;

    public class PizzaVariationDto : IResponseDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public PizzaDto Pizza { get; set; }

        [Required]
        public SizeDto Size { get; set; }

        [Required]
        public DoughDto Dough { get; set; }

        public IEnumerable<IngredientDto> Ingredients { get; set; }

        public IEnumerable<AdditionalIngredientDto> AdditionalIngredients { get; set; }

        [Required]
        [Range(0, 10000)]
        public decimal Price { get; set; }
    }
}
