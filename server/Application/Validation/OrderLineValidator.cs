namespace Application.Validation
{
    using System.Collections.Generic;
    using System.Linq;
    using Application.Interfaces;
    using Application.Interfaces.RequestDtoInterfaces;

    public class OrderLineValidator : BaseValidator
    {
        private IOrderLineService _orderLineService;

        public OrderLineValidator(IOrderLineService orderLineService)
        {
            _orderLineService = orderLineService;
        }

        public ValidationResult Validate(IOrderLineWithPizzaVariationAndOrder entity, IEnumerable<string> pizzasVariationsIds = null, IEnumerable<string> ordersIds = null)
        {
            ValidationResult annotationsValidationResult = ValidateAnnotations(entity);
            if (!annotationsValidationResult.IsValid)
            {
                return annotationsValidationResult;
            }

            if (entity.PizzaVariationId != null)
            {
                ValidationResult pizzaVariationsCoexistenceResult = PizzaVariationCoexistenceValidation(entity.PizzaVariationId, pizzasVariationsIds);
                if (!pizzaVariationsCoexistenceResult.IsValid)
                {
                    return pizzaVariationsCoexistenceResult;
                }
            }

            if (entity.OrderId != null)
            {
                ValidationResult orderCoexistenceResult = OrderCoexistenceValidation(entity.OrderId, ordersIds);
                if (!orderCoexistenceResult.IsValid)
                {
                    return orderCoexistenceResult;
                }
            }

            return new ValidationResult(true);
        }

        public ValidationResult PizzaVariationCoexistenceValidation(string pizzaVariationId, IEnumerable<string> pizzasVariationsIds)
        {
            if (!pizzasVariationsIds.Contains(pizzaVariationId))
            {
                return new ValidationResult(false, "There are no variation of pizza with identificator: " + pizzaVariationId);
            }

            return new ValidationResult(true);
        }

        public ValidationResult OrderCoexistenceValidation(string orderId, IEnumerable<string> ordersIds)
        {
            if (!ordersIds.Contains(orderId))
            {
                return new ValidationResult(false, "There are no order with identificator: " + orderId);
            }

            return new ValidationResult(true);
        }
    }
}
