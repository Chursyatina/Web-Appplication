namespace Application.DTO.Response
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Application.Interfaces;

    public class SizeDto : IResponseDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Range(0.1, 7)]
        public decimal PriceMultiplier { get; set; }
    }
}