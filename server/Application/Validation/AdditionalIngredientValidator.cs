namespace Application.Validation
{
    using Application.Interfaces;

    public class AdditionalIngredientValidator : BaseValidator
    {
        private IAdditionalIngredientService _additionalIngredientService;

        public AdditionalIngredientValidator(IAdditionalIngredientService additionalIngredientService)
        {
            _additionalIngredientService = additionalIngredientService;
        }

        public ValidationResult Validate(INamedRequestDto entity, string id = null)
        {
            ValidationResult annotationsValidationResult = ValidateAnnotations(entity);
            if (!annotationsValidationResult.IsValid)
            {
                return annotationsValidationResult;
            }

            ValidationResult uniqueNameValidationResult = UniqueNameValidation(entity, _additionalIngredientService, id);
            if (!uniqueNameValidationResult.IsValid)
            {
                return uniqueNameValidationResult;
            }

            return new ValidationResult(true);
        }
    }
}
