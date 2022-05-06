namespace Domain.Repository
{
    using System.Collections.Generic;
    using Domain.Models;

    public interface IOrderRepository : IRepository<Order>
    {
        public IEnumerable<string> GetIdentificators();

        public Order Insert(Order item, List<string> orderLinesIds);

        public Order Patch(string id, Order item, IEnumerable<string> orderLinesIds);

        public Order Update(string id, Order item, List<string> orderLinesIds);

        public Order Insert(Order item, List<string> orderLinesIds, User user);

        public IEnumerable<Order> GetOrdersForUserById(string id);
    }
}
