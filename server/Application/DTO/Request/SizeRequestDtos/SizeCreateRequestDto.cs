namespace Application.DTO.Request
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Application.Interfaces;

    public class SizeCreateRequestDto : INamedRequestDto
    {
        [Required]
        [StringLength(20, MinimumLength = 1)]
        public string Name { get; set; }

        [Required]
        [Range(0.1, 7)]
        public decimal PriceMultiplier { get; set; }
    }
}