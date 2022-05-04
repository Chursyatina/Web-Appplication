namespace Infrastructure.Repository
{
    using System.Collections.Generic;
    using System.Linq;
    using Application.Services;
    using Domain.Models;
    using Domain.Repository;
    using Infrastructure.EF;
    using Microsoft.EntityFrameworkCore;

    public class PizzaRepository : IPizzaRepository
    {
        private readonly DatabaseContext _context;

        public PizzaRepository(DatabaseContext context)
        {
            _context = context;
        }

        public Pizza Insert(Pizza item)
        {
            var entity = _context.Add(item);
            _context.SaveChanges();
            return entity.Entity;
        }

        public Pizza Insert(Pizza item, List<string> ingredientsIds)
        {
            item.Ingredients = new List<Ingredient>();

            foreach (string id in ingredientsIds)
            {
                item.Ingredients.Add(_context.Ingredients.FirstOrDefault(ing => ing.Id == id));
            }

            item.Price = PriceCountingService.GetStartingPriceForPizza(item);

            var entity = _context.Add(item);
            _context.SaveChanges();
            return entity.Entity;
        }

        public IEnumerable<Pizza> GetAll()
        {
            IEnumerable<Pizza> pizzas = new List<Pizza>(_context.Pizzas
              .Include(p => p.Ingredients)
              .Select(p => new Pizza()
              {
                  Id = p.Id,
                  Name = p.Name,
                  Description = p.Description,
                  IsDeleted = p.IsDeleted,
                  ImageLink = p.ImageLink,
                  SingleItemImageLink = p.SingleItemImageLink,
                  Ingredients = p.Ingredients,
                  Price = p.Price,
              }))
                .Where(p => p.IsDeleted == false)
                .AsQueryable()
                .AsNoTracking();

            return pizzas;
        }

        public Pizza GetById(string id)
        {
            Pizza pizza = _context.Pizzas
                .Include(pi => pi.Ingredients)
                .AsNoTracking()
                .FirstOrDefault(p => p.Id == id && p.IsDeleted == false);

            return pizza;
        }

        public Pizza GetByName(string name)
        {
            Pizza pizza = _context.Pizzas
                .Include(pi => pi.Ingredients)
                .AsNoTracking()
                .FirstOrDefault(p => p.Name == name && p.IsDeleted == false);

            return pizza;
        }

        public Pizza Update(string id, Pizza item, List<string> ingredientsIds)
        {
            var existingItem = _context.Pizzas
               .Include(pi => pi.Ingredients)
               .FirstOrDefault(p => p.Id == id && p.IsDeleted == false);
            existingItem.Name = item.Name;
            existingItem.Description = item.Description;
            existingItem.ImageLink = item.ImageLink;
            existingItem.SingleItemImageLink = item.SingleItemImageLink;

            if (ingredientsIds.Count != 0)
            {
                existingItem = ChangeIngredients(existingItem, ingredientsIds);
                existingItem.Price = PriceCountingService.GetStartingPriceForPizza(existingItem);
            }
            else
            {
                existingItem.Ingredients = new List<Ingredient>();
                existingItem.Price = PriceCountingService.GetStartingPriceForPizza(existingItem);
            }

            var entity = _context.Update(existingItem);
            _context.SaveChanges();

            return entity.Entity;
        }

        public Pizza Patch(string id, Pizza item, List<string> ingredientsIds)
        {
            var existingItem = _context.Pizzas
               .Include(pi => pi.Ingredients)
               .FirstOrDefault(p => p.Id == id && p.IsDeleted == false);

            if (item.Name != null)
            {
                existingItem.Name = item.Name;
            }

            if (item.Description != null)
            {
                existingItem.Description = item.Description;
            }

            if (item.ImageLink != null)
            {
                existingItem.ImageLink = item.ImageLink;
            }

            if (item.SingleItemImageLink != null)
            {
                existingItem.SingleItemImageLink = item.SingleItemImageLink;
            }

            if (item.Ingredients != null)
            {
                existingItem = ChangeIngredients(existingItem, ingredientsIds);
                existingItem.Price = PriceCountingService.GetStartingPriceForPizza(existingItem);
            }

            existingItem.Price = PriceCountingService.GetStartingPriceForPizza(existingItem);

            var entity = _context.Update(existingItem);
            _context.SaveChanges();

            return entity.Entity;
        }

        public void Delete(string id)
        {
            Pizza existingItem = _context.Pizzas.Find(id);
            existingItem.IsDeleted = !existingItem.IsDeleted;

            _context.SaveChanges();
        }

        public IEnumerable<string> GetIdentificators()
        {
            return _context.Pizzas.AsNoTracking().Select(p => p.Id);
        }

        private Pizza ChangeIngredients(Pizza existingItem, List<string> ingredientsIds)
        {
            List<string> existingItemIngredients = existingItem.Ingredients.Select(ing => ing.Id).ToList();

            IEnumerable<string> remains = existingItemIngredients.Intersect(ingredientsIds);

            IEnumerable<string> toRemove = existingItemIngredients.Except(remains);

            IEnumerable<string> toAdd = ingredientsIds.Except(remains);

            existingItem.Ingredients = existingItem.Ingredients.Where(i => !toRemove.Contains(i.Id)).ToList();

            foreach (string id in toAdd)
            {
                existingItem.Ingredients.Add(_context.Ingredients.FirstOrDefault(ing => ing.Id == id));
            }

            return existingItem;
        }
    }
}
