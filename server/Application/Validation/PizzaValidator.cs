namespace Application.Validation
{
    using System.Collections.Generic;
    using System.Linq;
    using Application.Interfaces;

    public class PizzaValidator : BaseValidator
    {
        private IPizzaService _pizzaService;

        public PizzaValidator(IPizzaService pizzaService)
        {
            _pizzaService = pizzaService;
        }

        public ValidationResult Validate(INamedPizzaWithIngredients entity, int id, IEnumerable<int> ingredientsIndentificators = null)
        {
            ValidationResult annotationsValidationResult = ValidateAnnotations(entity);
            if (!annotationsValidationResult.IsValid)
            {
                return annotationsValidationResult;
            }

            if (entity.Name != null)
            {
                ValidationResult uniqueNameValidationResult = UniqueNameValidation(entity, _pizzaService, id);
                if (!uniqueNameValidationResult.IsValid)
                {
                    return uniqueNameValidationResult;
                }
            }

            if (ingredientsIndentificators != null)
            {
                ValidationResult ingredientUniquenessValidationResult = IngredientUniquenessValidation(entity.Ingredients.ToList());
                if (!ingredientUniquenessValidationResult.IsValid)
                {
                    return ingredientUniquenessValidationResult;
                }

                foreach (int ingId in entity.Ingredients)
                {
                    if (!ingredientsIndentificators.Contains(ingId))
                    {
                        return new ValidationResult(false, "There is no ingredient with id: " + ingId);
                    }
                }
            }

            return new ValidationResult(true);
        }

        public ValidationResult Validate(INamedRequestDto entity)
        {
            ValidationResult annotationsValidationResult = ValidateAnnotations(entity);
            if (!annotationsValidationResult.IsValid)
            {
                return annotationsValidationResult;
            }

            ValidationResult uniqueNameValidationResult = UniqueNameValidation(entity, _pizzaService);
            if (!uniqueNameValidationResult.IsValid)
            {
                return uniqueNameValidationResult;
            }

            return new ValidationResult(true);
        }

        private ValidationResult IngredientUniquenessValidation(List<int> ingredientsIds)
        {
            ingredientsIds.Sort();
            int? current = null;
            foreach (int id in ingredientsIds)
            {
                if (current == id)
                {
                    return new ValidationResult(false, "The identifier: " + id + " is repeated more than 1 time");
                }

                current = id;
            }

            return new ValidationResult(true);
        }
    }
}
