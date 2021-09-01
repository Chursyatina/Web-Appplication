namespace Application.Validation
{
    public class ValidationResult
    {
        public ValidationResult(bool isValid, string errorMessage = null)
        {
            this.IsValid = isValid;
            this.ErrorMessage = errorMessage;
        }

        public ValidationResult()
        {
        }

        public bool IsValid { get; }

        public string ErrorMessage { get; }
    }
}
