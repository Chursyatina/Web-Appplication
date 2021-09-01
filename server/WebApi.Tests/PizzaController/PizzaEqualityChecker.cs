namespace WebApi.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using Application.DTO.Response;
    using Domain.Models;

    public static class PizzaEqualityChecker
    {
        public static bool IsListOfDtosEqualsListOfModels(List<PizzaDto> listDtos, List<Pizza> listModels)
        {
            if (listDtos.Count != listModels.Count)
            {
                return false;
            }

            for (int i = 0; i < listDtos.Count; i++)
            {
                if (!IsDtoEqualsModel(listDtos[i], listModels[i]))
                {
                    return false;
                }
            }

            return true;
        }

        public static bool IsDtoEqualsDto(PizzaDto firstDto, PizzaDto secondDto)
        {
            return firstDto.ImageLink == secondDto.ImageLink && firstDto.Name == secondDto.Name && firstDto.Description == secondDto.Description && IngredientEqualityChecker.IsListOfDtosEqualsListOfDtos(firstDto.Ingredients.ToList(), secondDto.Ingredients.ToList());
        }

        public static bool IsDtoEqualsModel(PizzaDto dto, Pizza model)
        {
            return dto.Name == model.Name && dto.ImageLink == model.ImageLink && dto.Description == model.Description && IngredientEqualityChecker.IsListOfDtosEqualsListOfModels(dto.Ingredients.ToList(), model.Ingredients.ToList());
        }
    }
}
