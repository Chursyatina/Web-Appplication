namespace Infrastructure.Repository
{
    using System.Collections.Generic;
    using System.Linq;
    using Domain.Models;
    using Domain.Repository;
    using Infrastructure.EF;
    using Microsoft.EntityFrameworkCore;

    public class AdditionalIngredientRepository : IAdditionalIngredientRepository
    {
        private DatabaseContext _context;

        public AdditionalIngredientRepository(DatabaseContext context)
        {
            _context = context;
        }

        public void Delete(string id)
        {
            AdditionalIngredient existingItem = _context.AdditionalIngredients.Find(id);
            existingItem.IsDeleted = !existingItem.IsDeleted;

            _context.SaveChanges();
        }

        public AdditionalIngredient GetById(string id)
        {
            return _context.AdditionalIngredients.AsNoTracking().FirstOrDefault(p => p.Id == id && !p.IsDeleted);
        }

        public AdditionalIngredient GetByName(string name)
        {
            return _context.AdditionalIngredients.AsNoTracking().FirstOrDefault(p => p.Name == name && !p.IsDeleted);
        }

        public IEnumerable<AdditionalIngredient> GetAll()
        {
            return _context.AdditionalIngredients.Where(p => p.IsDeleted == false).AsNoTracking();
        }

        public IEnumerable<string> GetIdentificators()
        {
            return _context.AdditionalIngredients.AsNoTracking().Select(ing => ing.Id);
        }

        public AdditionalIngredient Insert(AdditionalIngredient item)
        {
            var entity = _context.Add(item);
            _context.SaveChanges();
            return entity.Entity;
        }

        public AdditionalIngredient Update(string id, AdditionalIngredient item)
        {
            var existingItem = _context.AdditionalIngredients.Find(id);

            existingItem.Name = item.Name;
            existingItem.ImageLink = item.ImageLink;
            existingItem.Price = item.Price;
            existingItem.IsAvailable = item.IsAvailable;

            var entity = _context.Update(existingItem);
            _context.SaveChanges();
            return entity.Entity;
        }

        public AdditionalIngredient Patch(string id, AdditionalIngredient item)
        {
            var existingItem = _context.AdditionalIngredients.Find(id);

            if (item.Name != null)
            {
                existingItem.Name = item.Name;
            }

            if (item.ImageLink != null)
            {
                existingItem.ImageLink = item.ImageLink;
            }

            if (item.Price > 0)
            {
                existingItem.Price = item.Price;
            }

            var entity = _context.Update(existingItem);
            _context.SaveChanges();
            return entity.Entity;
        }
    }
}
