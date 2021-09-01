namespace Application.DTO.Request.OrderStatusRequestDtos
{
    using System.ComponentModel.DataAnnotations;
    using Application.Interfaces;

    public class OrderStatusPatchRequestDto : INamedRequestDto
    {
        [StringLength(20, MinimumLength = 1)]
        public string Name { get; set; }
    }
}
