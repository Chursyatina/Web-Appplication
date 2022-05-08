namespace Application.DTO.Response
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Application.Interfaces;

    public class PizzaDto : IResponseDto
    {
        [Required]
        public string Id { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 1)]
        public string Name { get; set; }

        [Required]
        [StringLength(150, MinimumLength = 10)]
        public string Description { get; set; }

        [Required]
        public string ImageLink { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

        [Required]
        public bool IsAvailable { get; set; }

        [Required]
        public string SingleItemImageLink { get; set; }

        [Required]
        public IEnumerable<IngredientDto> Ingredients { get; set; }
    }
}
