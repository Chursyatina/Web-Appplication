namespace WebApi.Tests
{
    using System.Collections.Generic;
    using Application.DTO.Response;
    using Domain.Models;

    public static class AdditionalIngredientEqualityChecker
    {
        public static bool IsListOfDtosEqualsListOfModels(List<AdditionalIngredientDto> listDtos, List<AdditionalIngredient> listModels)
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

        public static bool IsListOfDtosEqualsListOfDtos(List<AdditionalIngredientDto> firstListOfDtos, List<AdditionalIngredientDto> secondListOfDtos)
        {
            if (firstListOfDtos.Count != secondListOfDtos.Count)
            {
                return false;
            }

            for (int i = 0; i < firstListOfDtos.Count; i++)
            {
                if (!IsDtoEqualsDto(firstListOfDtos[i], secondListOfDtos[i]))
                {
                    return false;
                }
            }

            return true;
        }

        public static bool IsDtoEqualsDto(AdditionalIngredientDto firstDto, AdditionalIngredientDto secondDto)
        {
            return firstDto.Name == secondDto.Name && firstDto.ImageLink == secondDto.ImageLink && firstDto.Price == secondDto.Price;
        }

        public static bool IsDtoEqualsModel(AdditionalIngredientDto dto, AdditionalIngredient model)
        {
            return dto.Name == model.Name && dto.ImageLink == model.ImageLink && dto.Price == model.Price;
        }
    }
}
