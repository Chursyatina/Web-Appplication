namespace Application.DTO.Response
{
    using System.ComponentModel.DataAnnotations;
    using Application.Interfaces;

    public class OrderStatusDto : IResponseDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
