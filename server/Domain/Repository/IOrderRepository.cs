namespace Domain.Repository
{
    using System.Collections.Generic;
    using Domain.Models;

    public interface IOrderRepository : IRepository<Order>
    {
        public IEnumerable<int> GetIdentificators();

        public Order Insert(Order item);

        public Order Patch(int id, Order item, IEnumerable<int> orderLinesIds);

        public Order Update(int id, Order item, List<int> orderLinesIds);
    }
}
