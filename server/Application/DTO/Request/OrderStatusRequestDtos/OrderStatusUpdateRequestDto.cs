namespace Application.DTO.Request.OrderStatusRequestDtos
{
    using System.ComponentModel.DataAnnotations;
    using Application.Interfaces;

    public class OrderStatusUpdateRequestDto : INamedRequestDto
    {
        [Required]
        [StringLength(20, MinimumLength = 1)]
        public string Name { get; set; }
    }
}
