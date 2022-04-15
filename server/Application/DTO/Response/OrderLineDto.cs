namespace Application.DTO.Response
{
    using System.ComponentModel.DataAnnotations;
    using Application.Interfaces;

    public class OrderLineDto : IResponseDto
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public PizzaVariationDto PizzaVariation { get; set; }

        [Required]
        [Range(0.1, 100000)]
        public decimal Price { get; set; }

        [Required]
        [Range(0, 10)]
        public int Quantity { get; set; }
    }
}
