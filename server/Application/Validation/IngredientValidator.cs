namespace Application.Validation
{
    using Application.Interfaces;

    public class IngredientValidator : BaseValidator
    {
        private IIngredientService _ingredientService;

        public IngredientValidator(IIngredientService ingredientService)
        {
            _ingredientService = ingredientService;
        }

        public ValidationResult Validate(INamedRequestDto entity, string id = null)
        {
            ValidationResult annotationsValidationResult = ValidateAnnotations(entity);
            if (!annotationsValidationResult.IsValid)
            {
                return annotationsValidationResult;
            }

            ValidationResult uniqueNameValidationResult = UniqueNameValidation(entity, _ingredientService, id);
            if (!uniqueNameValidationResult.IsValid)
            {
                return uniqueNameValidationResult;
            }

            return new ValidationResult(true);
        }
    }
}
