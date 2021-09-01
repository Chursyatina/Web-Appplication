namespace Infrastructure.Repository
{
    using System.Collections.Generic;
    using System.Linq;
    using Domain.Models;
    using Domain.Repository;
    using Infrastructure.EF;
    using Microsoft.EntityFrameworkCore;

    public class DoughRepository : IDoughRepository
    {
        private readonly DatabaseContext _context;

        public DoughRepository(DatabaseContext context)
        {
            _context = context;
        }

        public void Delete(int id)
        {
            _context.Doughs.Find(id).IsDeleted = true;
            _context.SaveChanges();
        }

        public Dough GetById(int id)
        {
            return _context.Doughs.AsNoTracking().FirstOrDefault(d => d.Id == id && d.IsDeleted == false);
        }

        public Dough GetByName(string name)
        {
            return _context.Doughs.AsNoTracking().FirstOrDefault(p => p.Name == name && p.IsDeleted == false);
        }

        public IEnumerable<Dough> GetAll()
        {
            return _context.Doughs.AsNoTracking().Where(p => p.IsDeleted == false);
        }

        public Dough Insert(Dough item)
        {
            var entity = _context.Add(item);
            _context.SaveChanges();
            return entity.Entity;
        }

        public Dough Update(int id, Dough item)
        {
            var existingItem = _context.Doughs.Find(id);

            existingItem.Name = item.Name;
            existingItem.PriceMultiplier = item.PriceMultiplier;

            var entity = _context.Update(existingItem);
            _context.SaveChanges();
            return entity.Entity;
        }

        public Dough Patch(int id, Dough item)
        {
            var existingItem = _context.Doughs.Find(id);

            if (item.Name != null)
            {
                existingItem.Name = item.Name;
            }

            if (item.PriceMultiplier > 0)
            {
                existingItem.PriceMultiplier = item.PriceMultiplier;
            }

            var entity = _context.Update(existingItem);
            _context.SaveChanges();
            return entity.Entity;
        }

        public IEnumerable<int> GetIdentificators()
        {
            return _context.Doughs.AsNoTracking().Select(ing => ing.Id);
        }
    }
}
