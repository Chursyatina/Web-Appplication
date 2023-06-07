namespace Application.DTO.Response
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Application.Interfaces;

    public class DoughDto : IResponseDto
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

        [Required]
        [Range(0.1, 7)]
        public decimal PriceMultiplier { get; set; }
    }
}