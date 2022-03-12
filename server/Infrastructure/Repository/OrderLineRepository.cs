namespace Infrastructure.Repository
{
    using System.Collections.Generic;
    using System.Linq;
    using Application.Services;
    using Domain.Models;
    using Domain.Repository;
    using Infrastructure.EF;
    using Microsoft.EntityFrameworkCore;

    public class OrderLineRepository : IOrderLineRepository
    {
        private readonly DatabaseContext _context;

        public OrderLineRepository(DatabaseContext context)
        {
            _context = context;
        }

        public void Delete(int id)
        {
            _context.OrderLines.Find(id).IsDeleted = true;
            _context.SaveChanges();
        }

        public OrderLine GetById(int id)
        {
            OrderLine existingOrderLine = _context.OrderLines
                .Include(p => p.PizzaVariation).ThenInclude(o => o.Pizza)
                .Include(p => p.PizzaVariation).ThenInclude(o => o.Ingredients)
                .Include(p => p.PizzaVariation).ThenInclude(o => o.Dough)
                .Include(p => p.PizzaVariation).ThenInclude(o => o.Size)
                .Include(p => p.PizzaVariation).ThenInclude(o => o.Ingredients)
                .Include(p => p.PizzaVariation).ThenInclude(o => o.AdditionalIngredients)
                .AsNoTracking().FirstOrDefault(l => l.Id == id && l.IsDeleted == false);

            return existingOrderLine;
        }

        public IEnumerable<OrderLine> GetAll()
        {
            return _context.OrderLines
                .Include(p => p.PizzaVariation).ThenInclude(o => o.Pizza)
                .Include(p => p.PizzaVariation).ThenInclude(o => o.Ingredients)
                .Include(p => p.PizzaVariation).ThenInclude(o => o.Dough)
                .Include(p => p.PizzaVariation).ThenInclude(o => o.Size)
                .Include(p => p.PizzaVariation).ThenInclude(o => o.Ingredients)
                .Include(p => p.PizzaVariation).ThenInclude(o => o.AdditionalIngredients)
                .AsNoTracking();
        }

        public OrderLine Update(int id, OrderLine item, int pizzaVariationId)
        {
            var existingItem = _context.OrderLines
                .Include(o => o.Order)
                .Include(pv => pv.PizzaVariation).ThenInclude(ai => ai.AdditionalIngredients)
                .Include(pv => pv.PizzaVariation).ThenInclude(i => i.Ingredients)
                .Include(pv => pv.PizzaVariation).ThenInclude(p => p.Pizza).ThenInclude(ing => ing.Ingredients)
                .FirstOrDefault(o => o.Id == id && o.IsDeleted == false);

            existingItem.Quantity = item.Quantity;
            existingItem.PizzaVariation = _context.PizzasVariations.Find(pizzaVariationId);
            existingItem.Price = PriceCountingService.GetPriceForOrderLine(existingItem);
            existingItem.Order.Price = PriceCountingService.GetPriceForOrder(_context.Orders
                .Include(p => p.OrderStatus)
                .Include(l => l.OrderLines)
                .FirstOrDefault(or => or.Id == existingItem.Order.Id));

            var entity = _context.Update(existingItem);
            _context.SaveChanges();

            return entity.Entity;
        }

        public OrderLine Patch(int id, OrderLine item, int? pizzaVariationId)
        {
            var existingItem = _context.OrderLines
                .Include(o => o.Order)
                .Include(pv => pv.PizzaVariation).ThenInclude(ai => ai.AdditionalIngredients)
                .Include(pv => pv.PizzaVariation).ThenInclude(i => i.Ingredients)
                .Include(pv => pv.PizzaVariation).ThenInclude(p => p.Pizza).ThenInclude(ing => ing.Ingredients)
                .FirstOrDefault(o => o.Id == id && o.IsDeleted == false);

            if (item.Quantity > 0 && existingItem.Quantity != item.Quantity)
            {
                existingItem.Quantity = item.Quantity;
            }

            if (pizzaVariationId != null && pizzaVariationId != existingItem.PizzaVariation.Id)
            {
                existingItem.PizzaVariation = _context.PizzasVariations.Find(pizzaVariationId);
            }

            existingItem.Price = PriceCountingService.GetPriceForOrderLine(existingItem);
            existingItem.Order.Price = PriceCountingService.GetPriceForOrder(_context.Orders
                .Include(p => p.OrderStatus)
                .Include(l => l.OrderLines)
                .FirstOrDefault(or => or.Id == existingItem.Order.Id));

            var entity = _context.Update(existingItem);
            _context.SaveChanges();

            return entity.Entity;
        }

        public OrderLine Insert(OrderLine item, int pizzaVariationId, int orderId)
        {
            item.PizzaVariation = _context.PizzasVariations
                .Include(i => i.Ingredients)
                .Include(a => a.AdditionalIngredients)
                .Include(s => s.Size)
                .Include(d => d.Dough)
                .Include(p => p.Pizza)
                .ThenInclude(i => i.Ingredients)
                .FirstOrDefault(pv => pv.Id == pizzaVariationId && !pv.IsDeleted);

            item.Order = _context.Orders
                .Include(p => p.OrderStatus)
                .FirstOrDefault(or => or.Id == orderId && !or.IsDeleted);
            item.Quantity = 1;
            item.Price = PriceCountingService.GetPriceForOrderLine(item);

            item.Order.Price = PriceCountingService.GetPriceForOrder(item.Order);

            var entity = _context.Add(item);
            _context.SaveChanges();
            return entity.Entity;
        }

        public IEnumerable<int> GetIdentificators()
        {
            return _context.OrderLines.AsNoTracking().Select(l => l.Id);
        }
    }
}
