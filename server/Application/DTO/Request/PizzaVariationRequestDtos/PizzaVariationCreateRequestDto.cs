namespace Application.DTO.Request
{
    using System.ComponentModel.DataAnnotations;
    using Application.Interfaces;

    public class PizzaVariationCreateRequestDto : IPizzaVariationRequestDto
    {
        [Required]
        public string PizzaId { get; set; }

        [Required]
        public string SizeId { get; set; }

        [Required]
        public string DoughId { get; set; }
    }
}
