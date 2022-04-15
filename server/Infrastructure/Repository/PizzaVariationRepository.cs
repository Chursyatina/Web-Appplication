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

    public class PizzaVariationRepository : IPizzaVariationRepository
    {
        private readonly DatabaseContext _context;

        private IQueryable<PizzaVariation> _query;

        public PizzaVariationRepository(DatabaseContext context)
        {
            _context = context;

            _query = context.PizzasVariations
                .Include(p => p.Pizza)
                .ThenInclude(i => i.Ingredients)
                .Include(d => d.Dough)
                .Include(s => s.Size)
                .Include(i => i.Ingredients)
                .Include(a => a.AdditionalIngredients)
                .Where(l => !l.IsDeleted);
        }

        public void Delete(string id)
        {
            _context.PizzasVariations.Find(id).IsDeleted = true;
            _context.SaveChanges();
        }

        public PizzaVariation GetById(string id)
        {
            return _query.FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<PizzaVariation> GetAll()
        {
            return new List<PizzaVariation>(_query
                .Select(p => new PizzaVariation()
                {
                    Id = p.Id,
                    Price = p.Price,
                    Pizza = p.Pizza,
                    Dough = p.Dough,
                    Size = p.Size,
                    IsDeleted = p.IsDeleted,
                    Ingredients = p.Ingredients,
                    AdditionalIngredients = p.AdditionalIngredients,
                }))
                .AsQueryable()
                .AsNoTracking();
        }

        public PizzaVariation Insert(PizzaVariation item, string pizzaId, string sizeId, string doughId)
        {
            item.Pizza = _context.Pizzas
                .Include(pi => pi.Ingredients)
                .FirstOrDefault(p => p.Id == pizzaId && !p.IsDeleted);
            item.Size = _context.Sizes.FirstOrDefault(p => p.Id == sizeId && !p.IsDeleted);
            item.Dough = _context.Doughs.FirstOrDefault(p => p.Id == doughId && !p.IsDeleted);
            item.Ingredients.AddRange(item.Pizza.Ingredients);
            item.Price = PriceCountingService.GetPriceForPizzaVariation(item);

            var entity = _context.Add(item);
            _context.SaveChanges();
            return entity.Entity;
        }

        public PizzaVariation Patch(string id, PizzaVariation item, string pizzaId, string doughId, string sizeId, List<string> ingredientsIds, List<string> additionalIngredientsIds)
        {
            PizzaVariation pizzaVariation = _query
                .FirstOrDefault(p => p.Id == id);

            if (pizzaId != null && pizzaVariation.Pizza.Id != pizzaId)
            {
                pizzaVariation.Pizza = _context.Pizzas.Include(p => p.Ingredients).FirstOrDefault(p => p.Id == pizzaId);
                pizzaVariation.Ingredients = new List<Ingredient>();
                pizzaVariation.Ingredients.AddRange(pizzaVariation.Pizza.Ingredients);
            }
            else
            {
                if (ingredientsIds != null)
                {
                    pizzaVariation = ChangeIngredients(pizzaVariation, ingredientsIds.ToList());
                }
            }

            if (doughId != null && pizzaVariation.Dough.Id != doughId)
            {
                pizzaVariation.Dough = _context.Doughs.FirstOrDefault(d => d.Id == doughId);
            }

            if (sizeId != null && pizzaVariation.Size.Id != sizeId)
            {
                pizzaVariation.Size = _context.Sizes.FirstOrDefault(s => s.Id == sizeId);
            }

            if (additionalIngredientsIds != null)
            {
                pizzaVariation = ChangeAdditionalIngredients(pizzaVariation, additionalIngredientsIds.ToList());
            }

            pizzaVariation.Price = PriceCountingService.GetPriceForPizzaVariation(pizzaVariation);

            var entity = _context.Update(pizzaVariation);
            _context.SaveChanges();

            return entity.Entity;
        }

        public PizzaVariation Update(string id, PizzaVariation item, string pizzaId, string doughId, string sizeId, List<string> ingredientsIds, List<string> additionalIngredientsIds)
        {
            PizzaVariation pizzaVariation = _query
                .FirstOrDefault(p => p.Id == id);

            if (pizzaVariation.Pizza.Id != pizzaId)
            {
                pizzaVariation.Pizza = _context.Pizzas.Include(p => p.Ingredients).FirstOrDefault(p => p.Id == pizzaId);
                pizzaVariation.Ingredients = new List<Ingredient>();
                pizzaVariation.Ingredients.AddRange(pizzaVariation.Pizza.Ingredients);
            }
            else
            {
                pizzaVariation = ChangeIngredients(pizzaVariation, ingredientsIds);
            }

            pizzaVariation.Dough = _context.Doughs.FirstOrDefault(d => d.Id == doughId);
            pizzaVariation.Size = _context.Sizes.FirstOrDefault(s => s.Id == sizeId);

            pizzaVariation = ChangeAdditionalIngredients(pizzaVariation, additionalIngredientsIds);

            pizzaVariation.Price = PriceCountingService.GetPriceForPizzaVariation(pizzaVariation);

            var entity = _context.Update(pizzaVariation);
            _context.SaveChanges();

            return entity.Entity;
        }

        public PizzaVariation Insert(PizzaVariation item, string pizzaId, string sizeId, string doughId, List<string> ingredientsIds, List<string> additionalIngredientsIds)
        {
            item.Pizza = _context.Pizzas
                .Include(pi => pi.Ingredients)
                .FirstOrDefault(p => p.Id == pizzaId && !p.IsDeleted);
            item.Size = _context.Sizes.FirstOrDefault(p => p.Id == sizeId && !p.IsDeleted);
            item.Dough = _context.Doughs.FirstOrDefault(p => p.Id == doughId && !p.IsDeleted);
            item.Ingredients.AddRange(item.Pizza.Ingredients);

            if (ingredientsIds.Count != 0)
            {
                item = ChangeIngredients(item, ingredientsIds);
            }

            item = ChangeAdditionalIngredients(item, additionalIngredientsIds);

            item.Price = PriceCountingService.GetPriceForPizzaVariation(item);

            var entity = _context.Add(item);
            _context.SaveChanges();
            return entity.Entity;
        }

        public IEnumerable<string> GetIdentificators()
        {
            return _context.PizzasVariations.AsNoTracking().Select(pv => pv.Id);
        }

        private PizzaVariation ChangeIngredients(PizzaVariation existingItem, List<string> ingredientsIds)
        {
            List<string> existingItemIngredients = existingItem.Ingredients.Select(ing => ing.Id).ToList();

            List<string> remains = existingItemIngredients.Intersect(ingredientsIds).ToList();

            List<string> toRemove = existingItemIngredients.Except(remains).ToList();

            List<string> toAdd = ingredientsIds.Except(remains).ToList();

            existingItem.Ingredients = existingItem.Ingredients.Where(i => !toRemove.Contains(i.Id)).ToList();

            existingItem.Ingredients.AddRange(_context.Ingredients.Where(ing => toAdd.Contains(ing.Id)));

            return existingItem;
        }

        private PizzaVariation ChangeAdditionalIngredients(PizzaVariation existingItem, List<string> ingredientsIds)
        {
            List<string> existingItemIngredients = existingItem.AdditionalIngredients.Select(ing => ing.Id).ToList();

            List<string> remains = existingItemIngredients.Intersect(ingredientsIds).ToList();

            List<string> toRemove = existingItemIngredients.Except(remains).ToList();

            List<string> toAdd = ingredientsIds.Except(remains).ToList();

            existingItem.AdditionalIngredients = existingItem.AdditionalIngredients.Where(i => !toRemove.Contains(i.Id)).ToList();

            existingItem.AdditionalIngredients.AddRange(_context.AdditionalIngredients.Where(ing => toAdd.Contains(ing.Id)));

            return existingItem;
        }
    }
}
