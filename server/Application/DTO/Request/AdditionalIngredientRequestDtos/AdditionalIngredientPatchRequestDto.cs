﻿namespace Application.DTO.Request
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Application.Interfaces;

    public class AdditionalIngredientPatchRequestDto : INamedRequestDto
    {
        [StringLength(20, MinimumLength = 1)]
        public string Name { get; set; }

        [Range(0.1, 1000)]
        public decimal? Price { get; set; }

        public string ImageLink { get; set; }
    }
}
