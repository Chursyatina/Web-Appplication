namespace Application.Validation
{
    using System.Collections.Generic;
    using System.Linq;
    using Application.Interfaces.RequestDtoInterfaces;
    using Application.Interfaces.ServicesInterfaces;

    public class OrderValidator : BaseValidator
    {
        private IOrderService _orderService;

        public OrderValidator(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public ValidationResult Validate(IOrderRequestDtoWithOrderLines entity)
        {
            ValidationResult annotationsValidationResult = ValidateAnnotations(entity);
            if (!annotationsValidationResult.IsValid)
            {
                return annotationsValidationResult;
            }

            return new ValidationResult(true);
        }

        public ValidationResult Validate(IOrderRequestDtoWithOrderLines entity, IEnumerable<string> ordersLinesIds, IEnumerable<string> orderStatusesIds)
        {
            ValidationResult annotationsValidationResult = ValidateAnnotations(entity);
            if (!annotationsValidationResult.IsValid)
            {
                return annotationsValidationResult;
            }

            if (entity.OrderLinesIds != null)
            {
                ValidationResult orderLinesUniquenessValidation = OrderLinesUniquenessValidation(entity.OrderLinesIds.ToList());
                if (orderLinesUniquenessValidation.IsValid)
                {
                    return orderLinesUniquenessValidation;
                }

                foreach (string lineId in entity.OrderLinesIds)
                {
                    if (!ordersLinesIds.Contains(lineId))
                    {
                        return new ValidationResult(false, "There is no order line with id: " + lineId);
                    }
                }
            }

            if (entity.OrderStatusId != null)
            {
                ValidationResult orderStatusCoexistenceValidationResult = OrderStatusCoexistenceValidation(entity.OrderStatusId, orderStatusesIds);
                if (!orderStatusCoexistenceValidationResult.IsValid)
                {
                    return orderStatusCoexistenceValidationResult;
                }
            }

            return new ValidationResult(true);
        }

        private ValidationResult OrderLinesUniquenessValidation(List<string> orderLinesIds)
        {
            orderLinesIds.Sort();
            string current = null;
            foreach (string id in orderLinesIds)
            {
                if (current == id)
                {
                    return new ValidationResult(false, "The identifier: " + id + " is repeated more than 1 time");
                }

                current = id;
            }

            return new ValidationResult(true);
        }

        private ValidationResult OrderStatusCoexistenceValidation(string orderStatudId, IEnumerable<string> ordersStatuses)
        {
            if (!ordersStatuses.Contains(orderStatudId))
            {
                return new ValidationResult(false, "There are no order status with identificator: " + orderStatudId);
            }

            return new ValidationResult(true);
        }
    }
}
