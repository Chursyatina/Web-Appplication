namespace Domain.Repository
{
    using System.Collections.Generic;
    using Domain.Models;

    public interface IOrderStatusRepository : IRepository<OrderStatus>
    {
        public OrderStatus GetByName(string name);

        public OrderStatus Patch(string id, OrderStatus item);

        public IEnumerable<string> GetIdentificators();

        public OrderStatus Insert(OrderStatus item);

        public OrderStatus Update(string id, OrderStatus item);
    }
}
