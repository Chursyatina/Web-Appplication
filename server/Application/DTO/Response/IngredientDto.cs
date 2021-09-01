namespace Application.DTO.Response
{
    using System.ComponentModel.DataAnnotations;
    using Application.Interfaces;

    public class IngredientDto : IResponseDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 1)]
        public string Name { get; set; }

        [Required]
        public string ImageLink { get; set; }

        [Required]
        [Range(0.1, 100)]
        public decimal Price { get; set; }
    }
}
