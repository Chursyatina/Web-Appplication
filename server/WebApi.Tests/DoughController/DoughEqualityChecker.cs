namespace WebApi.Tests
{
    using System.Collections.Generic;
    using Application.DTO.Response;
    using Domain.Models;

    public static class DoughEqualityChecker
    {
        public static bool IsListOfDtosEqualsListOfModels(List<DoughDto> listDtos, List<Dough> listModels)
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

        public static bool IsDtoEqualsDto(DoughDto firstDto, DoughDto secondDto)
        {
            return firstDto.Name == secondDto.Name && firstDto.PriceMultiplier == secondDto.PriceMultiplier;
        }

        public static bool IsDtoEqualsModel(DoughDto dto, Dough model)
        {
            return dto.Name == model.Name && dto.PriceMultiplier == model.PriceMultiplier;
        }
    }
}
