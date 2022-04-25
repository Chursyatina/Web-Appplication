namespace Application.Services
{
    using Domain.Models;

    public static class PriceCountingService
    {
        public const decimal BasicPizzaPrice = 200;

        public static decimal GetStartingPriceForPizza(Pizza pizza)
        {
            decimal price = BasicPizzaPrice;

            foreach (Ingredient ingredient in pizza.Ingredients)
            {
                if (!ingredient.IsDeleted)
                {
                    price += ingredient.Price;
                }
            }

            return price;
        }

        public static decimal GetPriceForPizzaVariation(PizzaVariation pizzaVariation)
        {
            decimal price = BasicPizzaPrice;

            price *= pizzaVariation.Dough.PriceMultiplier;

            foreach (Ingredient ingredient in pizzaVariation.Ingredients)
            {
                if (!ingredient.IsDeleted)
                {
                    price += ingredient.Price;
                }
            }

            foreach (AdditionalIngredient additionalIngredient in pizzaVariation.AdditionalIngredients)
            {
                if (!additionalIngredient.IsDeleted)
                {
                    price += additionalIngredient.Price;
                }
            }

            price *= pizzaVariation.Size.PriceMultiplier;

            return price;
        }

        public static decimal GetPriceForOrderLine(OrderLine orderLine)
        {
            return orderLine.PizzaVariation.Price * orderLine.Quantity;
        }

        public static decimal GetPriceForOrder(Order order)
        {
            decimal price = 0;

            foreach (OrderLine orderLine in order.OrderLines)
            {
                if (!orderLine.IsDeleted)
                {
                    price += orderLine.Price;
                }
            }

            return price;
        }

        public static decimal GetPriceForBasket(Basket basket)
        {
            decimal price = 0;

            foreach (OrderLine basketLine in basket.OrderLines)
            {
                if (basketLine.IsDeleted)
                {
                    price += basketLine.Price;
                }
            }

            return price;
        }
    }
}
