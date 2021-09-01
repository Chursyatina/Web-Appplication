namespace Infrastructure.Repository
{
    using System.Collections.Generic;
    using System.Linq;
    using Domain.Models;
    using Domain.Repository;
    using Infrastructure.EF;
    using Microsoft.EntityFrameworkCore;

    public class OrderStatusRepository : IOrderStatusRepository
    {
        private readonly DatabaseContext _context;

        public OrderStatusRepository(DatabaseContext context)
        {
            _context = context;
        }

        public void Delete(int id)
        {
            _context.OrderStatuses.Find(id).IsDeleted = true;
            _context.SaveChanges();
        }

        public OrderStatus GetById(int id)
        {
            return _context.OrderStatuses.AsNoTracking().FirstOrDefault(d => d.Id == id && d.IsDeleted == false);
        }

        public IEnumerable<OrderStatus> GetAll()
        {
            return _context.OrderStatuses.AsNoTracking().Where(p => p.IsDeleted == false);
        }

        public OrderStatus Insert(OrderStatus item)
        {
            var entity = _context.Add(item);
            _context.SaveChanges();
            return entity.Entity;
        }

        public OrderStatus Update(int id, OrderStatus item)
        {
            var existingItem = _context.OrderStatuses.Find(id);

            existingItem.Name = item.Name;

            var entity = _context.Update(existingItem);
            _context.SaveChanges();
            return entity.Entity;
        }

        public OrderStatus Patch(int id, OrderStatus item)
        {
            var existingItem = _context.OrderStatuses.Find(id);

            if (item.Name != null)
            {
                existingItem.Name = item.Name;
            }

            var entity = _context.Update(existingItem);
            _context.SaveChanges();
            return entity.Entity;
        }

        public OrderStatus GetByName(string name)
        {
            return _context.OrderStatuses.AsNoTracking().FirstOrDefault(o => o.Name == name && o.IsDeleted == false);
        }

        public IEnumerable<int> GetIdentificators()
        {
            return _context.OrderStatuses.AsNoTracking().Select(ing => ing.Id);
        }
    }
}
