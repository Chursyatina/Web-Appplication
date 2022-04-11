namespace Application.Interfaces.ServicesInterfaces
{
    using System.Collections.Generic;
    using Application.DTO.Request.BasketRequestDtos;
    using Application.DTO.Response;
    using Domain.Models;

    public interface IBasketService : IService<BasketDto, BasketCreateRequestDto, BasketUpdateRequestDto, BasketPatchRequestDto>
    {
        public IEnumerable<Basket> GetModels();

        public Basket UpdateByModel(Basket basket);
    }
}
