namespace Infrastructure.Repository
{
    using System.Collections.Generic;
    using System.Linq;
    using Domain.Models;
    using Domain.Repository;
    using Infrastructure.EF;
    using Microsoft.EntityFrameworkCore;

    public class IngredientRepository : IIngredientRepository
    {
        private readonly DatabaseContext _context;

        public IngredientRepository(DatabaseContext context)
        {
            _context = context;
        }

        public void Delete(int id)
        {
            _context.Ingredients.Find(id).IsDeleted = true;
            _context.SaveChanges();
        }

        public Ingredient GetById(int id)
        {
            return _context.Ingredients.AsNoTracking().FirstOrDefault(p => p.Id == id && p.IsDeleted == false);
        }

        public Ingredient GetByName(string name)
        {
            return _context.Ingredients.AsNoTracking().FirstOrDefault(p => p.Name == name && p.IsDeleted == false);
        }

        public IEnumerable<Ingredient> GetAll()
        {
            return _context.Ingredients.AsNoTracking().Where(p => p.IsDeleted == false);
        }

        public IQueryable<Ingredient> GetIngredientsById(List<int> identificators)
        {
            return _context.Ingredients.AsNoTracking().Where(ing => identificators.Contains(ing.Id));
        }

        public List<int> GetIdentificators()
        {
            return _context.Ingredients.AsNoTracking().Select(ing => ing.Id).ToList();
        }

        public Ingredient Insert(Ingredient item)
        {
            var entity = _context.Add(item);
            _context.SaveChanges();
            return entity.Entity;
        }

        public Ingredient Update(int id, Ingredient item)
        {
            var existingItem = _context.Ingredients.Find(id);

            existingItem.Name = item.Name;
            existingItem.ImageLink = item.ImageLink;
            existingItem.Price = item.Price;

            var entity = _context.Update(existingItem);
            _context.SaveChanges();
            return entity.Entity;
        }

        public Ingredient Patch(int id, Ingredient item)
        {
            var existingItem = _context.Ingredients.Find(id);

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
