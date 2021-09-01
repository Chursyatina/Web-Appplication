namespace Application.Validation
{
    using Application.Interfaces;

    public class DoughValidator : BaseValidator
    {
        private IDoughService _doughService;

        public DoughValidator(IDoughService doughService)
        {
            _doughService = doughService;
        }

        public ValidationResult Validate(INamedRequestDto entity, int? id = null)
        {
            ValidationResult annotationsValidationResult = ValidateAnnotations(entity);
            if (!annotationsValidationResult.IsValid)
            {
                return annotationsValidationResult;
            }

            ValidationResult uniqueNameValidationResult = UniqueNameValidation(entity, _doughService, id);
            if (!uniqueNameValidationResult.IsValid)
            {
                return uniqueNameValidationResult;
            }

            return new ValidationResult(true);
        }
    }
}