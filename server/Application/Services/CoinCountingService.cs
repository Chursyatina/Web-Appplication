namespace Application.Services
{
    using Domain.Models;

    public static class CoinCountingService
    {
        public static int GetCoins(Order order)
        {
            decimal coins = 0;

            foreach (OrderLine line in order.OrderLines)
            {
                coins += line.Price * (decimal)line.PizzaVariation.Pizza.BonusCoef;
            }

            return (int)coins;
        }
    }
}
