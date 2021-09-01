namespace Application.Validation
{
    using Application.Interfaces;

    public class SizeValidator : BaseValidator
    {
        private ISizeService _sizeService;

        public SizeValidator(ISizeService sizeService)
        {
            _sizeService = sizeService;
        }

        public ValidationResult Validate(INamedRequestDto entity, int? id = null)
        {
            ValidationResult annotationsValidationResult = ValidateAnnotations(entity);
            if (!annotationsValidationResult.IsValid)
            {
                return annotationsValidationResult;
            }

            ValidationResult uniqueNameValidationResult = UniqueNameValidation(entity, _sizeService, id);
            if (!uniqueNameValidationResult.IsValid)
            {
                return uniqueNameValidationResult;
            }

            return new ValidationResult(true);
        }
    }
}