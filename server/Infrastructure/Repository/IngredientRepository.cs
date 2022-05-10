namespace Infrastructure.Repository
{
    using System.Collections.Generic;
    using System.Linq;
    using Application.Services;
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

        public void Delete(string id)
        {
            Ingredient existingItem = _context.Ingredients.Find(id);
            existingItem.IsDeleted = !existingItem.IsDeleted;
            foreach (Pizza pizza in _context.Pizzas.Include(i => i.Ingredients))
            {
                pizza.Price = PriceCountingService.GetStartingPriceForPizza(pizza);
            }

            _context.SaveChanges();
        }

        public Ingredient GetById(string id)
        {
            return _context.Ingredients.AsNoTracking().FirstOrDefault(p => p.Id == id);
        }

        public Ingredient GetByName(string name)
        {
            return _context.Ingredients.AsNoTracking().FirstOrDefault(p => p.Name == name);
        }

        public IEnumerable<Ingredient> GetAll()
        {
            return _context.Ingredients.AsNoTracking();
        }

        public IQueryable<Ingredient> GetIngredientsById(List<string> identificators)
        {
            return _context.Ingredients.AsNoTracking().Where(ing => identificators.Contains(ing.Id));
        }

        public List<string> GetIdentificators()
        {
            return _context.Ingredients.AsNoTracking().Select(ing => ing.Id).ToList();
        }

        public Ingredient Insert(Ingredient item)
        {
            var entity = _context.Add(item);
            _context.SaveChanges();
            return entity.Entity;
        }

        public Ingredient Update(string id, Ingredient item)
        {
            var existingItem = _context.Ingredients.Find(id);

            existingItem.Name = item.Name;
            existingItem.ImageLink = item.ImageLink;
            existingItem.Price = item.Price;
            existingItem.IsDeleted = item.IsDeleted;
            existingItem.IsAvailable = item.IsAvailable;
            existingItem.IsObligatory = item.IsObligatory;

            var entity = _context.Update(existingItem);

            foreach (Pizza pizza in _context.Pizzas.Include(i => i.Ingredients))
            {
                if (pizza.Ingredients.Contains(existingItem))
                {
                    pizza.Price = PriceCountingService.GetStartingPriceForPizza(pizza);
                    pizza.IsAvailable = AvailabnessCheckingService.GetAvialebnessForPizza(pizza);
                }
            }

            _context.SaveChanges();
            return entity.Entity;
        }

        public Ingredient Patch(string id, Ingredient item)
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
            foreach (Pizza pizza in _context.Pizzas.Include(i => i.Ingredients))
            {
                if (pizza.Ingredients.Contains(existingItem))
                {
                    pizza.Price = PriceCountingService.GetStartingPriceForPizza(pizza);
                    pizza.IsAvailable = AvailabnessCheckingService.GetAvialebnessForPizza(pizza);
                }
            }

            _context.SaveChanges();
            return entity.Entity;
        }
    }
}
