namespace Domain.Repository
{
    using System.Collections.Generic;
    using Domain.Models;

    public interface IOrderStatusRepository : IRepository<OrderStatus>
    {
        public OrderStatus GetByName(string name);

        public OrderStatus Patch(int id, OrderStatus item);

        public IEnumerable<int> GetIdentificators();

        public OrderStatus Insert(OrderStatus item);

        public OrderStatus Update(int id, OrderStatus item);
    }
}
