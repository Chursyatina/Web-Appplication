namespace Application.Validation
{
    using System.Collections.Generic;
    using System.Linq;
    using Application.Interfaces;

    public class PizzaVariationValidator : BaseValidator
    {
        private IPizzaVariationService _pizzaVariationService;

        public PizzaVariationValidator(IPizzaVariationService pizzaVariationService)
        {
            _pizzaVariationService = pizzaVariationService;
        }

        public ValidationResult Validate(IPizzaVariationRequestDto entity, IEnumerable<string> pizzasIds, IEnumerable<string> sizesIds, IEnumerable<string> doughsIds)
        {
            ValidationResult annotationsValidationResult = ValidateAnnotations(entity);
            if (!annotationsValidationResult.IsValid)
            {
                return annotationsValidationResult;
            }

            if (entity.PizzaId != null)
            {
                ValidationResult pizzaСoexistenceValidationResult = PizzaСoexistenceValidation(entity.PizzaId, pizzasIds);
                if (!pizzaСoexistenceValidationResult.IsValid)
                {
                    return pizzaСoexistenceValidationResult;
                }
            }

            if (entity.SizeId != null)
            {
                ValidationResult sizeСoexistenceValidationResult = SizeСoexistenceValidation(entity.SizeId, sizesIds);
                if (!sizeСoexistenceValidationResult.IsValid)
                {
                    return sizeСoexistenceValidationResult;
                }
            }

            if (entity.DoughId != null)
            {
                ValidationResult doughСoexistenceValidationResult = DoughСoexistenceValidation(entity.DoughId, doughsIds);
                if (!doughСoexistenceValidationResult.IsValid)
                {
                    return doughСoexistenceValidationResult;
                }
            }

            return new ValidationResult(true);
        }

        public ValidationResult Validate(
            IPizzaVariationWithBothIngredientsTypes entity,
            IEnumerable<string> pizzasIds,
            IEnumerable<string> sizesIds,
            IEnumerable<string> doughsIds,
            IEnumerable<string> ingredientsIds,
            IEnumerable<string> additionalIngredientsIds)
        {
            ValidationResult annotationsValidationResult = ValidateAnnotations(entity);
            if (!annotationsValidationResult.IsValid)
            {
                return annotationsValidationResult;
            }

            if (entity.PizzaId != null)
            {
                ValidationResult pizzaСoexistenceValidationResult = PizzaСoexistenceValidation(entity.PizzaId, pizzasIds);
                if (!pizzaСoexistenceValidationResult.IsValid)
                {
                    return pizzaСoexistenceValidationResult;
                }
            }

            if (entity.SizeId != null)
            {
                ValidationResult sizeСoexistenceValidationResult = SizeСoexistenceValidation(entity.SizeId, sizesIds);
                if (!sizeСoexistenceValidationResult.IsValid)
                {
                    return sizeСoexistenceValidationResult;
                }
            }

            if (entity.DoughId != null)
            {
                ValidationResult doughСoexistenceValidationResult = DoughСoexistenceValidation(entity.DoughId, doughsIds);
                if (!doughСoexistenceValidationResult.IsValid)
                {
                    return doughСoexistenceValidationResult;
                }
            }

            if (entity.Ingredients != null)
            {
                ValidationResult ingredientUniquenessValidationResult = IngredientUniquenessValidation(entity.Ingredients.ToList());
                if (!ingredientUniquenessValidationResult.IsValid)
                {
                    return ingredientUniquenessValidationResult;
                }

                foreach (string ingId in entity.Ingredients)
                {
                    if (!ingredientsIds.Contains(ingId))
                    {
                        return new ValidationResult(false, "There is no ingredient with id: " + ingId);
                    }
                }
            }

            if (entity.AdditionalIngredients != null)
            {
                ValidationResult ingredientUniquenessValidationResult = IngredientUniquenessValidation(entity.AdditionalIngredients.ToList());
                if (!ingredientUniquenessValidationResult.IsValid)
                {
                    return ingredientUniquenessValidationResult;
                }

                foreach (string ingId in entity.AdditionalIngredients)
                {
                    if (!additionalIngredientsIds.Contains(ingId))
                    {
                        return new ValidationResult(false, "There is no additional ingredient with id: " + ingId);
                    }
                }
            }

            return new ValidationResult(true);
        }

        public ValidationResult PizzaСoexistenceValidation(string pizzaId, IEnumerable<string> pizzasIds)
        {
            if (!pizzasIds.Contains(pizzaId))
            {
                return new ValidationResult(false, "There are no pizza with identificator: " + pizzaId);
            }

            return new ValidationResult(true);
        }

        public ValidationResult SizeСoexistenceValidation(string sizeId, IEnumerable<string> sizesIds)
        {
            if (!sizesIds.Contains(sizeId))
            {
                return new ValidationResult(false, "There are no size with identificator: " + sizeId);
            }

            return new ValidationResult(true);
        }

        public ValidationResult DoughСoexistenceValidation(string doughId, IEnumerable<string> doughsIds)
        {
            if (!doughsIds.Contains(doughId))
            {
                return new ValidationResult(false, "There are no pizza with identificator: " + doughId);
            }

            return new ValidationResult(true);
        }

        private ValidationResult IngredientUniquenessValidation(List<string> ingredientsIds)
        {
            foreach (string id in ingredientsIds)
            {
                if (ingredientsIds.LastIndexOf(id) != ingredientsIds.IndexOf(id))
                {
                    return new ValidationResult(false, "The identifier: " + id + " is repeated more than 1 time");
                }
            }

            return new ValidationResult(true);
        }
    }
}
