namespace Application.DTO.Request
{
    using System.ComponentModel.DataAnnotations;
    using Application.Interfaces;

    public class PizzaVariationCreateRequestDto : IPizzaVariationRequestDto
    {
        [Required]
        public int? PizzaId { get; set; }

        [Required]
        public int? SizeId { get; set; }

        [Required]
        public int? DoughId { get; set; }
    }
}
