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
            OrderLine existingOrderLine = _context.OrderLines.AsNoTracking().FirstOrDefault(l => l.Id == id && l.IsDeleted == false);

            return existingOrderLine;
        }

        public IEnumerable<OrderLine> GetAll()
        {
            return _context.OrderLines.AsNoTracking();
        }

        public OrderLine Update(int id, OrderLine item, int pizzaVariationId)
        {
            var existingItem = _context.OrderLines
                .Include(pv => pv.PizzaVariation)
                .FirstOrDefault(o => o.Id == id && o.IsDeleted == false);

            existingItem.Quantity = item.Quantity;
            existingItem.PizzaVariation = _context.PizzasVariations.Find(pizzaVariationId);
            item.Price = PriceCountingService.GetPriceForOrderLine(item);

            var entity = _context.Update(existingItem);
            _context.SaveChanges();

            return entity.Entity;
        }

        public OrderLine Patch(int id, OrderLine item, int? pizzaVariationId)
        {
            var existingItem = _context.OrderLines
                .Include(pv => pv.PizzaVariation)
                .FirstOrDefault(o => o.Id == id && o.IsDeleted == false);

            if (item.Quantity > 0 && existingItem.Quantity != item.Quantity)
            {
                existingItem.Quantity = item.Quantity;
            }

            if (pizzaVariationId != null && pizzaVariationId != existingItem.PizzaVariation.Id)
            {
                existingItem.PizzaVariation = _context.PizzasVariations.Find(pizzaVariationId);
            }

            item.Price = PriceCountingService.GetPriceForOrderLine(item);

            var entity = _context.Update(existingItem);
            _context.SaveChanges();

            return entity.Entity;
        }

        public OrderLine Insert(OrderLine item, int pizzaVariationId)
        {
            item.PizzaVariation = _context.PizzasVariations
                .Include(i => i.Ingredients)
                .Include(a => a.AdditionalIngredients)
                .Include(s => s.Size)
                .Include(d => d.Dough)
                .Include(p => p.Pizza)
                .ThenInclude(i => i.Ingredients)
                .FirstOrDefault(pv => pv.Id == pizzaVariationId && !pv.IsDeleted);
            item.Quantity = 1;
            item.Price = PriceCountingService.GetPriceForOrderLine(item);

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
