namespace Infrastructure.Repository
{
    using System.Collections.Generic;
    using System.Linq;
    using Application.Services;
    using Domain.Models;
    using Domain.Repository;
    using Infrastructure.EF;
    using Infrastructure.ExtensionMethods;
    using Microsoft.EntityFrameworkCore;

    public class OrderRepository : IOrderRepository
    {
        private readonly DatabaseContext _context;

        public OrderRepository(DatabaseContext context)
        {
            _context = context;
        }

        public void Delete(string id)
        {
            _context.Orders.Find(id).IsDeleted = true;
            _context.SaveChanges();
        }

        public IEnumerable<Order> GetAll()
        {
            return _context.Orders
                .Include(s => s.OrderStatus)
                .Include(l => l.OrderLines).ThenInclude(m => m.PizzaVariation).ThenInclude(p => p.Pizza)
                .Include(l => l.OrderLines).ThenInclude(m => m.PizzaVariation).ThenInclude(p => p.Ingredients)
                .Include(l => l.OrderLines).ThenInclude(m => m.PizzaVariation).ThenInclude(p => p.Dough)
                .Include(l => l.OrderLines).ThenInclude(m => m.PizzaVariation).ThenInclude(p => p.Size)
                .Include(l => l.OrderLines).ThenInclude(m => m.PizzaVariation).ThenInclude(p => p.Ingredients)
                .Include(l => l.OrderLines).ThenInclude(m => m.PizzaVariation).ThenInclude(p => p.AdditionalIngredients)
                .Select(o => new Order()
            {
                Id = o.Id,
                IsDeleted = o.IsDeleted,
                OrderLines = o.OrderLines,
                Date = o.Date,
                OrderStatus = o.OrderStatus,
                Price = o.Price,
            })
                .Where(p => p.IsDeleted == false)
                .AsNoTracking();
        }

        public Order GetById(string id)
        {
            Order existingOrder = _context.Orders
                .Include(s => s.OrderStatus)
                .Include(l => l.OrderLines).ThenInclude(m => m.PizzaVariation).ThenInclude(p => p.Pizza)
                .Include(l => l.OrderLines).ThenInclude(m => m.PizzaVariation).ThenInclude(p => p.Ingredients)
                .Include(l => l.OrderLines).ThenInclude(m => m.PizzaVariation).ThenInclude(p => p.Dough)
                .Include(l => l.OrderLines).ThenInclude(m => m.PizzaVariation).ThenInclude(p => p.Size)
                .Include(l => l.OrderLines).ThenInclude(m => m.PizzaVariation).ThenInclude(p => p.Ingredients)
                .Include(l => l.OrderLines).ThenInclude(m => m.PizzaVariation).ThenInclude(p => p.AdditionalIngredients)
                .AsNoTracking()
                .FirstOrDefault(o => o.Id == id && o.IsDeleted == false);

            return existingOrder;
        }

        public Order Insert(Order item)
        {
            item.OrderStatus = _context.OrderStatuses.FirstOrDefault(s => s.Name == "Готовится");
            item.OrderLines = new List<OrderLine>();

            item.Date = System.DateTime.Now;
            item.Price = PriceCountingService.GetPriceForOrder(item);

            var entity = _context.Add(item);
            _context.SaveChanges();
            return entity.Entity;
        }

        public Order Patch(string id, Order item, IEnumerable<string> orderLinesIds)
        {
            var existingItem = _context.Orders
                .Include(o => o.OrderLines)
                .ThenInclude(pv => pv.PizzaVariation).ThenInclude(ai => ai.AdditionalIngredients)
                .Include(o => o.OrderLines)
                .ThenInclude(pv => pv.PizzaVariation).ThenInclude(i => i.Ingredients)
                .Include(o => o.OrderLines)
                .ThenInclude(pv => pv.PizzaVariation).ThenInclude(p => p.Pizza).ThenInclude(ing => ing.Ingredients)
                .Include(o => o.OrderStatus)
                .FirstOrDefault(o => o.Id == id && o.IsDeleted == false);

            if (item.OrderStatus.Name != null && item.OrderStatus != existingItem.OrderStatus)
            {
                existingItem.OrderStatus = item.OrderStatus;
            }

            if (orderLinesIds != null)
            {
                ChangeOrderLines(existingItem, orderLinesIds);
                existingItem.Price = PriceCountingService.GetPriceForOrder(existingItem);
            }

            existingItem.Price = PriceCountingService.GetPriceForOrder(existingItem);

            var entity = _context.Update(existingItem);
            _context.SaveChanges();

            return entity.Entity;
        }

        public Order Update(string id, Order item, List<string> orderLinesIds)
        {
            var existingItem = _context.Orders
                .Include(o => o.OrderLines)
                .ThenInclude(pv => pv.PizzaVariation).ThenInclude(ai => ai.AdditionalIngredients)
                .Include(o => o.OrderLines)
                .ThenInclude(pv => pv.PizzaVariation).ThenInclude(i => i.Ingredients)
                .Include(o => o.OrderLines)
                .ThenInclude(pv => pv.PizzaVariation).ThenInclude(p => p.Pizza).ThenInclude(ing => ing.Ingredients)
                .Include(o => o.OrderStatus)
                .FirstOrDefault(p => p.Id == id && p.IsDeleted == false);

            existingItem.OrderStatus = item.OrderStatus;
            existingItem.Date = item.Date;

            if (orderLinesIds.Count != 0)
            {
                existingItem = ChangeOrderLines(existingItem, orderLinesIds);
                existingItem.Price = PriceCountingService.GetPriceForOrder(existingItem);
            }
            else
            {
                existingItem.OrderLines = new List<OrderLine>();
            }

            existingItem.Price = PriceCountingService.GetPriceForOrder(existingItem);

            var entity = _context.Update(existingItem);
            _context.SaveChanges();

            return entity.Entity;
        }

        public IEnumerable<string> GetIdentificators()
        {
            return _context.Orders.AsNoTracking().Select(l => l.Id);
        }

        private Order ChangeOrderLines(Order existingItem, IEnumerable<string> orderLinesIds)
        {
            List<string> existingItemOrderLines = existingItem.OrderLines.Select(line => line.Id).ToList();

            List<string> remains = existingItemOrderLines.Intersect(orderLinesIds).ToList();

            List<string> toRemove = existingItemOrderLines.Except(remains).ToList();

            List<string> toAdd = orderLinesIds.Except(remains).ToList();

            existingItem.OrderLines = existingItem.OrderLines.Where(l => !toRemove.Contains(l.Id)).ToList();

            existingItem.OrderLines.AddRange(_context.OrderLines.Where(line => toAdd.Contains(line.Id)));

            return existingItem;
        }
    }
}
