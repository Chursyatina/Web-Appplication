namespace Application.DTO.Request
{
    using System.ComponentModel.DataAnnotations;
    using Application.Interfaces;

    public class PizzaCreateRequestDto : INamedRequestDto
    {
        [Required]
        [StringLength(20, MinimumLength = 1)]
        public string Name { get; set; }

        [Required]
        [StringLength(150, MinimumLength = 10)]
        public string Description { get; set; }

        [Required]
        public string ImageLink { get; set; }

        [Required]
        public string SingleItemImageLink { get; set; }
    }
}
