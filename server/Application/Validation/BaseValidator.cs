namespace Application.Validation
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Application.Interfaces;
    using Application.Interfaces.ServicesInterfaces;

    public class BaseValidator
    {
        protected ValidationResult ValidateAnnotations(object entity)
        {
            var results = new List<System.ComponentModel.DataAnnotations.ValidationResult>();
            var context = new ValidationContext(entity);
            if (!Validator.TryValidateObject(entity, context, results, true))
            {
                foreach (var resultError in results)
                {
                    return new ValidationResult(false, resultError.ErrorMessage);
                }
            }

            return new ValidationResult(true);
        }

        protected ValidationResult UniqueNameValidation<TIResponseDto>(INamedRequestDto entity, INamedEntityService<TIResponseDto> service, string updatingEntityId = null)
            where TIResponseDto : IResponseDto
        {
            IResponseDto existingEntity = service.GetByName(entity.Name);

            if (existingEntity != null && existingEntity.Id != updatingEntityId)
            {
                return new ValidationResult(false, "Enity with such name already exists");
            }

            return new ValidationResult(true);
        }
    }
}
