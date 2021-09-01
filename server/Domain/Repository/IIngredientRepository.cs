namespace Domain.Repository
{
    using System.Collections.Generic;
    using System.Linq;
    using Domain.Models;

    public interface IIngredientRepository : IRepository<Ingredient>
    {
        public Ingredient GetByName(string name);

        public List<int> GetIdentificators();

        public IQueryable<Ingredient> GetIngredientsById(List<int> identificators);

        public Ingredient Patch(int id, Ingredient item);

        public Ingredient Insert(Ingredient item);

        public Ingredient Update(int id, Ingredient item);
    }
}
