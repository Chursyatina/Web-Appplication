namespace Application.DTO.Request
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Application.Interfaces;

    public class IngredientUpdateRequestDto : INamedRequestDto
    {
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
    }
}
