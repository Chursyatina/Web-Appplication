namespace Infrastructure.Repository
{
    using System.Collections.Generic;
    using System.Linq;
    using Domain.Models;
    using Domain.Repository;
    using Infrastructure.EF;
    using Microsoft.EntityFrameworkCore;

    public class BasketRepository : IBasketRepository
    {
        private readonly DatabaseContext _context;

        public BasketRepository(DatabaseContext context)
        {
            _context = context;
        }

        public void Delete(int id)
        {
            _context.Baskets.Find(id).IsDeleted = true;
            _context.SaveChanges();
        }

        public IEnumerable<Basket> GetAll()
        {
            return _context.Baskets.AsNoTracking().Where(p => p.IsDeleted == false);
        }

        public Basket GetById(int id)
        {
            return _context.Baskets.AsNoTracking().FirstOrDefault(d => d.Id == id && d.IsDeleted == false);
        }

        public Basket UpdateByModel(Basket basket)
        {
            var existingItem = _context.Baskets.Find(basket.Id);

            existingItem.OrderLines = basket.OrderLines;

            var entity = _context.Update(existingItem);
            _context.SaveChanges();
            return entity.Entity;
        }
    }
}
