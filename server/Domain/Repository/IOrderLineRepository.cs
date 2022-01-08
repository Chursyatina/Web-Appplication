namespace Domain.Repository
{
    using System.Collections.Generic;
    using Domain.Models;

    public interface IOrderLineRepository : IRepository<OrderLine>
    {
        public IEnumerable<int> GetIdentificators();

        public OrderLine Insert(OrderLine item, int pizzaVariationId, int orderId);

        public OrderLine Patch(int id, OrderLine item, int? pizzaVariationId);

        public OrderLine Update(int id, OrderLine item, int pizzaVariationId);
    }
}
