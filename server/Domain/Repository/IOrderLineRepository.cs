namespace Domain.Repository
{
    using System.Collections.Generic;
    using Domain.Models;

    public interface IOrderLineRepository : IRepository<OrderLine>
    {
        public IEnumerable<string> GetIdentificators();

        public OrderLine Insert(OrderLine item, string pizzaVariationId, string orderId);

        public OrderLine InsertToBasket(OrderLine item, string pizzaVariationId, string basketId);

        public OrderLine Patch(string id, OrderLine item, string pizzaVariationId);

        public OrderLine Update(string id, OrderLine item, string pizzaVariationId);
    }
}
