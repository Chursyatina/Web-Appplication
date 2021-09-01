namespace Domain.Repository
{
    using System.Collections.Generic;
    using Domain.Models;

    public interface IAdditionalIngredientRepository : IRepository<AdditionalIngredient>
    {
        public AdditionalIngredient GetByName(string name);

        public AdditionalIngredient Patch(int id, AdditionalIngredient item);

        public IEnumerable<int> GetIdentificators();

        public AdditionalIngredient Insert(AdditionalIngredient item);

        public AdditionalIngredient Update(int id, AdditionalIngredient item);
    }
}
