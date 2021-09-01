﻿namespace Infrastructure.Repository
{
    using System.Collections.Generic;
    using System.Linq;
    using Domain.Models;
    using Domain.Repository;
    using Infrastructure.EF;
    using Microsoft.EntityFrameworkCore;

    public class SizeRepository : ISizeRepository
    {
        private readonly DatabaseContext _context;

        public SizeRepository(DatabaseContext context)
        {
            _context = context;
        }

        public Size Insert(Size item)
        {
            var entity = _context.Add(item);
            _context.SaveChanges();
            return entity.Entity;
        }

        public IEnumerable<Size> GetAll()
        {
            return _context.Sizes.AsNoTracking().Where(p => p.IsDeleted == false);
        }

        public Size GetById(int id)
        {
            return _context.Sizes.AsNoTracking().FirstOrDefault(d => d.Id == id && d.IsDeleted == false);
        }

        public Size Update(int id, Size item)
        {
            var existingItem = _context.Sizes.Find(id);

            existingItem.Name = item.Name;
            existingItem.PriceMultiplier = item.PriceMultiplier;

            var entity = _context.Update(existingItem);
            _context.SaveChanges();
            return entity.Entity;
        }

        public void Delete(int id)
        {
            _context.Sizes.Find(id).IsDeleted = true;
            _context.SaveChanges();
        }

        public Size GetByName(string name)
        {
            return _context.Sizes.AsNoTracking().FirstOrDefault(p => p.Name == name && p.IsDeleted == false);
        }

        public Size Patch(int id, Size item)
        {
            var existingItem = _context.Sizes.Find(id);

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
            return _context.Sizes.AsNoTracking().Select(ing => ing.Id);
        }
    }
}
