namespace Domain.Repository
{
    using System.Collections.Generic;
    using System.Linq;
    using Domain.Models;

    public interface IIngredientRepository : IRepository<Ingredient>
    {
        public Ingredient GetByName(string name);

        public List<string> GetIdentificators();

        public IQueryable<Ingredient> GetIngredientsById(List<string> identificators);

        public Ingredient Patch(string id, Ingredient item);

        public Ingredient Insert(Ingredient item);

        public Ingredient Update(string id, Ingredient item);
    }
}
